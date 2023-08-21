using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Application.Manager.Interface;
using Library.Domain.Entities;
using Library.Domain.Interface;
using Library.Infrastructure.Service;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Library.Infrastructure.Service.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Application.Manager.Implementation
{
	public class StudentManager : IStudentManager
	{
		private readonly IStudentService _service;
		private readonly IMemberService _memberService;
		private readonly IMapper _mapper;

		public StudentManager(IStudentService studentService, IMapper mapper, IMemberService memberService)
		{
			_service = studentService;
			_mapper = mapper;
			_memberService = memberService;
		}
		public async Task<ServiceResult<bool>> CreateStudent(StudentRequest studentRequest)
		{
			try
			{
				var model = _mapper.Map<EStudent>(studentRequest);

				if (model == null)
				{
					return new ServiceResult<bool>()
					{
						Data = false,
						Status = StatusType.Failure,
						Message = "Error while mapping"
					};
				}
				model.IsActive = true;
				model.IsDeleted = false;
				model.CreatedDate = DateTime.Now.ToUniversalTime();  //Done due to handle UTC Exceptions

				if (!IsValidEmail(model.Email))
				{
					return new ServiceResult<bool>()
					{
						Data = false,
						Status = StatusType.Failure,
						Message = "Email need to have special character."
					};
				}
				var CheckEmail = await _service.IsUniqueEmail(studentRequest.Email);
				if (CheckEmail == true)
				{
					Log.Error("Enter Unique Email: {Student}", JsonSerializer.Serialize(studentRequest));
					return new ServiceResult<bool>()
					{
						Data = false,
						Status = StatusType.Failure,
						Message = "Enter Unique Email"
					};
				}
				var result = await _service.CreateStudent(model);

				//adding the staff information in Member table
				EMember member = new EMember()
				{
					Email = studentRequest.Email,
					FullName = studentRequest.FullName,
					MemberType = Domain.Enum.MemberType.Student,
					//may not store Id
					MemberCode = studentRequest.StudentCode,
					ReferenceId = studentRequest.Id,
				};
				await _memberService.CreateMember(member);
				return new ServiceResult<bool>()
				{
					Data = true,
					Status = StatusType.Success,
					Message = "User Created Successfull"
				};
			}
			catch (Exception ex)
			{
				return new ServiceResult<bool>()
				{
					Data = false,
					Status = StatusType.Failure,
					Message = "Something went wrong"
				};
			}
		}

		public async Task<ServiceResult<bool>> DeleteStudent(int id)
		{
			try
			{
				var result = await _service.DeleteStudent(id);
				if (result == false)
				{
					return new ServiceResult<bool>()
					{
						Data = result,
						Status = StatusType.Failure,
						Message = "User not found"
					};
				}
				return new ServiceResult<bool>()
				{
					Data = result,
					Status = StatusType.Success,
					Message = "User Delete Successfull"
				};
			}
			catch (Exception ex)
			{
				return new ServiceResult<bool>()
				{
					Data = false,
					Status = StatusType.Failure,
					Message = "Something went wrong"
				};
			}
		}

		public async Task<ServiceResult<StudentResponse>> GetStudentByID(int id)
		{
			try
			{
				var user = await _service.GetStudentByID(id);
				if (user == null)
				{
					return new ServiceResult<StudentResponse>()
					{
						Data = new StudentResponse() { Id = id },
						Status = StatusType.Failure,
						Message = "User not found"
					};
				}
				if (user.IsDeleted == true)
				{
					return new ServiceResult<StudentResponse>()
					{
						Data = new StudentResponse() { Id = id },
						Status = StatusType.Failure,
						Message = "User was deleted"
					};
				}
				var result = new StudentResponse()
				{
					Id = user.Id,
					Email = user.Email,
					Faculty = user.Faculty,
					FullName = user.FullName,
					RollNo = user.RollNo,
					StudentCode = user.StudentCode,
				};

				return new ServiceResult<StudentResponse>()
				{
					Data = result,
					Status = StatusType.Success,
					Message = "User Found Successfull"
				};
			}
			catch (Exception ex)
			{
				return new ServiceResult<StudentResponse>()
				{
					Data = new StudentResponse(),
					Status = StatusType.Failure,
					Message = "Something went wrong"
				};
			}
		}

		public async Task<ServiceResult<List<StudentResponse>>> GetStudents()
		{
			try
			{
				var user = await _service.GetStudents();
				var result = (from u in user
							  where u.IsDeleted == false
							  orderby u.Id descending
							  select new StudentResponse()
							  {
								  Id = u.Id,
								  Email = u.Email,
								  Faculty = u.Faculty,
								  FullName = u.FullName,
								  RollNo = u.RollNo,
								  StudentCode = u.StudentCode
							  }
							 ).ToList();
				return new ServiceResult<List<StudentResponse>>()
				{
					Data = result,
					Status = StatusType.Success,
					Message = "Users List"
				};
			}
			catch (Exception ex)
			{
				return new ServiceResult<List<StudentResponse>>()
				{
					Data = new List<StudentResponse>(),
					Status = StatusType.Failure,
					Message = "Something went wrong"
				};
			}
		}

		public async Task<ServiceResult<bool>> UpdateStudent(StudentRequest studentRequest)
		{
			try
			{
				var model = _mapper.Map<EStudent>(studentRequest);
				//model.Id = studentRequest.Id;

				//var model = new EStudent()
				//{
				//	Id = studentRequest.Id,
				//	Email = studentRequest.Email,
				//	FullName = studentRequest.FullName,
				//	Faculty = studentRequest.Faculty,
				//	RollNo = studentRequest.RollNo,
				//	StudentCode = studentRequest.StudentCode,
				//	UpdatedBy = studentRequest.UpdatedBy,
				//};

				var user = await _service.UpdateStudent(model);
				return new ServiceResult<bool>()
				{
					Data = user,
					Status = user == true ? StatusType.Success : StatusType.Failure,
					Message = user == true ? "Student updated successfull" : "Student not found"
				};

			}
			catch (Exception ex)
			{
				return new ServiceResult<bool>()
				{
					Data = false,
					Status = StatusType.Failure,
					Message = "Something went wrong"
				};
			}

		}

		private bool IsValidEmail(string email)
		{
			const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			return Regex.IsMatch(email, emailPattern);
		}
	}
}
