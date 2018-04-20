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
                    return StatusCode(StatusCodes.Status409Conflict, ErrorCode.TodoItemIDInUse.ToString());
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
                
                
                _toDoRepository.Insert(emplogs);
                _toDoRepository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotCreateItem.ToString());
            }
            return Ok(emplogs);
        }
        [HttpPut]
        public IActionResult Edit([FromBody] ToDoItems item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest(ErrorCode.TodoItemNameAndNotesRequired.ToString());
                }
                var existingItem = _toDoRepository.Find(item.ID);
                if (existingItem == null)
                {
                    return NotFound(ErrorCode.RecordNotFound.ToString());
                }
                _toDoRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest(ErrorCode.CouldNotUpdateItem.ToString());
            }
            return NoContent();
        }
        public enum ErrorCode
        {
            TodoItemNameAndNotesRequired,
            TodoItemIDInUse,
            RecordNotFound,
            CouldNotCreateItem,
            CouldNotUpdateItem,
            CouldNotDeleteItem
        }
    }
}