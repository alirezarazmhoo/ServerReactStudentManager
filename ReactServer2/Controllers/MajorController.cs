using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactServer2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactServer2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MajorController : ControllerBase
	{
		private readonly ReactServer2Context _context;
		public MajorController(ReactServer2Context context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Majors.ToList());
		}

	}
}
