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
                    return BadRequest(ErrorCode.enterrecorddetails.ToString());
                }
                //bool empExits = _toDoRepository.DoesEmpExist(emp.EmpId);
                //if (empExits)
                //{
                //    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.logExist.ToString());
                //}
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
                    return BadRequest(ErrorCode.enterrecorddetails.ToString());
                }
                bool EmpLogExist = _toDoRepository.checklogexist(emplogs.EmpId);
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
        [HttpPost]
        [Route("CheckingsLogs")]
        public IActionResult ChecksCreatedLogs([FromBody] Checkings checkings)
        {

            try
            {
                if (checkings == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.enterrecorddetails.ToString());
                }
                //bool ChecksLogExist = _toDoRepository.CheckSupervisorLogs(checkings.SupId);
                //if (ChecksLogExist)
                //{
                //    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.logExist.ToString());
                //}
                _toDoRepository.Insert(checkings);
                _toDoRepository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(checkings);
        }
        [HttpGet]
        [Route("GetEmployee/{id}")]
        public IActionResult GetEmp(int id)
        {

            var employee = _toDoRepository.GetEmployee(id);
            return Ok(employee);
        }

       
        [HttpGet]
        [Route("GetEmployeeLogs/{id}")]
        public IActionResult Logs(int id)
        {
            
            var logs = _toDoRepository.Logs(id);
            if (logs == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }
            return Ok(logs);
        }

        [HttpGet]
        [Route("GetSupervisor/{id}")]
        public IActionResult Supervispor(int id)
        {

            var supervisor = _toDoRepository.GetSup(id);
            if (supervisor == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }
            return Ok(supervisor);
        }

        [HttpGet]
        [Route("GetStations/{name}")]
        public IActionResult GetStations(string name)
        {

            var station = _toDoRepository.GetStations(name);
            if (station == null)
            {
                return NotFound(ErrorCode.RecordNotFound.ToString());
            }

            return Ok(station);
        }
        [HttpPut]
        [Route("updatelogsstatus/{id}")]
        public IActionResult Edit(int id,[FromBody] EmployeesLogs employeesLogs)
        {
            try
            {
                if (employeesLogs == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.enterrecorddetails.ToString());
                }
                var RecordToBeUpdated = _toDoRepository.find(id);
                RecordToBeUpdated.CheckedSatus = employeesLogs.CheckedSatus;

                if (RecordToBeUpdated == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.UpdateEmployee(RecordToBeUpdated);
                _toDoRepository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }
        public enum ErrorCode
        {
            enterrecorddetails,
            logExist,
            RecordNotFound,
            CouldNotCreateItem,
            CouldNotUpdateItem,
            CouldNotDeleteItem
        }
    }
}