using Microsoft.AspNetCore.Mvc;
using RemoteControl.API.Models;
using System;
using System.Diagnostics;

namespace RemoteControl.API.Controllers
{
    [ApiController]
    [Route("commands")]
    public class CommandController : ControllerBase
    {
        private static Process _openedProcess;

        [HttpPost]
        public IActionResult Send([FromBody] CommandViewModel command)
        {
            try
            {
                if (_openedProcess != null)
                {
                    _openedProcess.Kill();
                }

                _openedProcess = Process.Start(new ProcessStartInfo(command.Url)
                {
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok();
        }
    }
}
