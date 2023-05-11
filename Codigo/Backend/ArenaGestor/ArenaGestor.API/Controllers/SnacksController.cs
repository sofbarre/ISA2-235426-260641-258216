using ArenaGestor.API.Filters;
using ArenaGestor.APIContracts;
using ArenaGestor.APIContracts.Artist;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ArenaGestor.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ExceptionFilter]
    public class SnacksController : ControllerBase, ISnackAppService
    { 
        private readonly IMapper mapper;
        private readonly SnackService snackService;
        public SnacksController(IMapper mapper, SnackService snackService)
        {
            this.mapper = mapper;
            this.snackService = snackService;   
        }
        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet]
        public IActionResult GetSnacks()
        {
            var result = snackService.GetSnacks();
            return Ok(result);
        }
        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPost]
        public IActionResult PostSnack([FromBody] SnackPostDto snack)
        {
            try
            {
                Snack mapSnack = new Snack()
                {
                    Name = snack.Name,
                    Description = snack.Description,
                    Price = snack.Price,
                };
                snackService.InsertSnack(mapSnack);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpPut("{snackId}")]
        public IActionResult PutSnack([FromRoute]int snackId,[FromBody] SnackPutDto snack)
        {
            try
            {
                Snack mapSnack = new Snack()
                {
                    SnackId = snackId,
                    Name = snack.Name,
                    Description = snack.Description,
                    Price = snack.Price,
                };
                snackService.UpdateSnack(mapSnack);
                return Ok();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpGet("{snackId}")]
        public IActionResult GetSnackById([FromRoute] int snackId)
        {
            try
            {
                var result = snackService.GetSnackById(snackId);
                return Ok(result);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [AuthorizationFilter(RoleCode.Administrador)]
        [HttpDelete("{snackId}")]
        public IActionResult DeleteSnack([FromRoute]int snackId)
        {
            try
            {
                snackService.DeleteSnack(snackId);
                return Ok();
            }catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
