// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// //Caminho da rota é o nome da Classe e suas chamadas interiores sem o "Controller"

// namespace recados_api
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class TestController : ControllerBase
//     {

//         private readonly ILogger<TestController> _logger;

//         public TestController(ILogger<TestController> logger)
//         {
//             _logger = logger;
//         }

//         [HttpGet]
//         public IActionResult Get()
//         {
//             var response = new Testa
//             {
//                 Message = "API funcional e rodando!"
//             };
//             return Ok(response);
//         }
//     }
// }
