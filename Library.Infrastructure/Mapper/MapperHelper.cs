using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Infrastructure.Mapper
{
	public static class MapperHelper
	{
		private static IMapper _mapper;
		public static bool _isUnitTest = false;

		public static IMapper Mapper => _mapper;

		public static void Configure(IMapper mapper)
		{
			_mapper = mapper;
		}
	}
}
