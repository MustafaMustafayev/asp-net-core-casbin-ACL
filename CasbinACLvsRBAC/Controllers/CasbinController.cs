using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasbinACLvsRBAC.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCasbin;

namespace CasbinACLvsRBAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasbinController : ControllerBase
    {
        private readonly DatabaseContext _context;  
        private readonly Enforcer _enforcer;
        public CasbinController(DatabaseContext context)
        {
            _context = context;
            var efAdapter = new CasbinDbAdapter<int>(_context);
            _enforcer = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
        }

        [HttpGet("checkRbacWithoutDomainAndRole")]
        public IActionResult checkRbacWithoutDomain(string userName, string domain, string permissionName)
        {
            var response = _enforcer.Enforce(userName, domain, permissionName);
            return Ok(response);
        }

        [HttpGet("checkRbacWithoutDomainButWithRole")]
        public IActionResult checkRbacWithoutDomainButWithRole(string userName, string domain, string permissionName)
        {
            var response = _enforcer.Enforce(userName, domain, permissionName);
            return Ok(response);
        }

        [HttpGet("checkRbacPerUserPerObject")]
        public IActionResult checkRbacPerUserPerObject(string userName, string domain, string objectId, string permissionName)
        {
            var response = _enforcer.HasPermissionForUser(userName, domain, objectId, permissionName);
            return Ok(response);
        }

        [HttpGet("checkRbacPerUserPerObjectByOjectType")]
        public IActionResult checkRbacPerUserPerObjectByOjectType(string userName, string domain, string objectId, string permissionName, string objectType)
        {
            var response = _enforcer.HasPermissionForUser(userName, domain, objectId, permissionName, objectType);
            return Ok(response);
        }

        [HttpPost("addPermissionWithoutDomain")]
        public IActionResult addPermissionWithoutDomain(string userName, string domain, string permissionName)
        {
            bool response = _enforcer.AddPolicy(userName, domain, permissionName);
            return Ok(response);
        }

        [HttpPost("addPermissionWithDomain")]
        public IActionResult addPermissionWithDomain(string userName, string domain, string objectId, string permissionName)
        {
            bool response = _enforcer.AddPolicy(userName, domain, objectId, permissionName);
            return Ok(response);
        }

        [HttpPost("addPermissionPerObjectByObjectType")]
        public IActionResult addPermissionPerObjectByObjectType(string userName, string domain, string objectId, string permissionName, string objectType)
        {
            bool response = _enforcer.AddPolicy(userName, domain, objectId, permissionName, objectType);
            return Ok(response);
        }

        [HttpPost("addGeneralRoleWithoutDomain")]
        public IActionResult addGeneralRoleWithoutModel(string userName, string roleName)
        {
            bool response = _enforcer.AddRoleForUser(userName, roleName);
            return Ok(response);
        }

        [HttpPost("addGeneralRoleWithDomain")]
        public IActionResult addGeneralRoleWithDomain(string userName, string domain, string roleName)
        {
            bool response = _enforcer.AddRoleForUserInDomain(userName, roleName, domain);
            return Ok(response);
        }

        [HttpDelete("deleteRoleWithoutDomain")]
        public IActionResult deleteRoleWithoutDomain(string userName, string roleName)
        {
            bool response = _enforcer.DeleteRoleForUser(userName, roleName);
            return Ok(response);
        }

        [HttpDelete("deletePermissionWithoutObject")]
        public IActionResult deletePermissionWithoutObject(string userName, string domain, string permissionName)
        {
            bool response = _enforcer.DeleteRoleForUserInDomain(userName, domain, permissionName);
            return Ok(response);
        }

        [HttpDelete("deletePermissionWithObject")]
        public IActionResult deletePermissionWithObject(string userName, string domain, string objectId, string permissionName)
        {
            bool response = _enforcer.DeletePermissionForUser(userName, domain, objectId, permissionName);
            return Ok(response);
        }

        [HttpGet("objectsByUser/{user}")]
        public IActionResult objectsByUser(string user, string domain)
        {
            var userPersmissions = _enforcer.GetPermissionsForUser(user);
            var permissionsByDomain = _enforcer.GetPermissionsForUserInDomain(user, domain);
            return Ok(userPersmissions);
        }
    }
}