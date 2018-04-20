using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSlBackendWebService.Interfaces;
using OSlBackendWebService.Models;

namespace OSlBackendWebService.Controllers
{
    [Produces("application/json")]
    [Route("api/OslWebservice")]
    public class OslWebserviceController : Controller
    {
        private readonly IOSLRepository _toDoRepository;

        public OslWebserviceController(IOSLRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;

        }
        [HttpGet]
        [Route("Employess")]
        public IActionResult List()
        {
            return Ok(_toDoRepository.GetAll);
        }
        [HttpPost]
        [Route("CreateNewEmployee")]
        public IActionResult Create([FromBody] Employees emp)
        {
            try
            {
                if (emp == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                bool empExits = _toDoRepository.DoesEmpExist(emp.EmpId);
                if (empExits)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.logExist.ToString());
                }
                _toDoRepository.Insert(emp);
                _toDoRepository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(emp);
        }
        [HttpPost]
        [Route("CreateEmployeeslog")]
        public IActionResult LogsCreate([FromBody] EmployeesLogs emplogs)
        {

            try
            {
                if (emplogs == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                bool EmpLogExist = _toDoRepository.DoesEmpExist(emplogs.EmpId);
                if (EmpLogExist)
                {
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.logExist.ToString());
                }
                _toDoRepository.Insert(emplogs);
                _toDoRepository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(emplogs);
        }
        [HttpGet]
        [Route("GetEmployee/{id}")]
        public IActionResult GetEmp(int id)
        {

            var employee = _toDoRepository.GetEmployee(id);
            return Ok(employee);
        }

        // get 
        [HttpGet]
        [Route("GetEmployeeLogs/{date}")]
        public IActionResult GetEmpLogs(DateTime date)
        {

            var logs = _toDoRepository.GetLogsByDate(date);
            return Ok(logs);
        }

        [HttpGet]
        [Route("GetSupervisor/{id}")]
        public IActionResult Supervispor(int id)
        {

            var supervisor = _toDoRepository.GetSup(id);

            return Ok(supervisor);
        }

        [HttpGet]
        [Route("GetStations/{id}")]
        public IActionResult GetStations(int id)
        {

            var station = _toDoRepository.GetStations(id);

            return Ok(station);
        }
        public enum ErrorCode
        {
            TodoItemNameAndNotesRequired,
            logExist,
            RecordNotFound,
            CouldNotCreateItem,
            CouldNotUpdateItem,
            CouldNotDeleteItem
        }
    }
}