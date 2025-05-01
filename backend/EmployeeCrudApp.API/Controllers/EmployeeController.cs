using EmployeeCrudApp.Application;
using EmployeeCrudApp.Application.Dtos;
using EmployeeCrudApp.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeCrudApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<GetEmployeeDto>>> GetEmployeeById(int id)
        {
            var response= new ResponseDto<GetEmployeeDto>();
            try
            {
                if (id > 0)
                {
                    var emp = await _employeeService.GetEmployeeById(id);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = emp;
                    return Ok(response);
                }
                else
                {
                    response.Error = "Id must be greater than 0";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }

        }
        [HttpGet("{page},{pageSize}")]
        public async Task<ActionResult<ResponseDto<PagedResult<GetEmployeeDto>>>> GetAllEmployees(int page, int pageSize)
        {
            var response = new ResponseDto<PagedResult<GetEmployeeDto>>();
            try
            {
                var emp = await _employeeService.GetAllEmployees(page, pageSize);
                response.StatusCode = HttpStatusCode.OK;
                response.Result = emp;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto<string>>> AddEmployee([FromBody] CreateEmployeeDto employee)
        {
            var response = new ResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.AddEmployee(employee);
                    response.StatusCode = HttpStatusCode.Created;
                    response.Result = "Employee created successfully";
                    return Created();
                }
                else
                {
                    response.Error = "Invalid model state";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (BadRequestException ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }
        }
        [HttpPut]
        public async Task<ActionResult<ResponseDto<string>>> UpdateEmployee([FromBody] UpdateEmployeeDto employee)
        {
            var response = new ResponseDto<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.UpdateEmployee(employee);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = "Employee updated successfully";
                    return Ok(response);
                }
                else
                {
                    response.Error = "Invalid model state";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess= false;
                    return BadRequest(response);
                }
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (BadRequestException ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteEmployee(int id)
        {
            var response = new ResponseDto<string>();
            try
            {
                if (id > 0)
                {
                    await _employeeService.DeleteEmployee(id);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = "Employee deleted successfully";
                    return Ok(response);
                }
                else
                {
                    response.Error = "Id must be greater than 0";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (NotFoundException ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }
            
        }
        [HttpGet("search/{search},{page},{pageSize}")]
        public async Task<ActionResult<ResponseDto<PagedResult<GetEmployeeDto>>>> SearchEmployees(string search, int page, int pageSize)
        {
            var response = new ResponseDto<PagedResult<GetEmployeeDto>>();
            try
            {
                if (search != null && search.Length > 0)
                {
                    var emp = await _employeeService.SearchEmployees(search, page, pageSize);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = emp;
                    return Ok(response);
                }
                else
                {
                    response.Error = "Search string cannot be null or empty";
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Error = ex.Message;
                return response;
            }
        }
    }
}
