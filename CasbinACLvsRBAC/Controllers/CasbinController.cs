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
        public CasbinController(DatabaseContext context)
        {
            _context = context;
        }


        [HttpGet("checkRbacWithoutDomainAndRole")]
        public IActionResult checkRbacWithoutDomain(string userName, string modulName, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            var response = e.Enforce(userName, modulName, permissionName);
            return Ok(response);
        }

        [HttpGet("checkRbacWithoutDomainButWithRole")]
        public IActionResult checkRbacWithoutDomainButWithRole(string userName, string modulName, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            var response = e.Enforce(userName, modulName, permissionName);

            return Ok(response);
        }

        //[HttpGet("checkRbacWitDomainAndWithRole")]
        //public IActionResult checkRbacWitDomainAndWithRole(string userName, string modulName, string permissionName)
        //{
        //    var efAdapter = new CasbinDbAdapter<int>(_context);
        //    Enforcer e = new Enforcer("CasbinConfig/RBAC_WITHOUT_DOMAIN/rbac_model.conf", efAdapter);
        //    var response = e.Enforce(userName, modulName, permissionName);
        //    return Ok(response);
        //}

        [HttpGet("checkRbacPerUserPerObject")]
        public IActionResult checkRbacPerUserPerObject(string userName, string modulName, string objectId, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            var response = e.HasPermissionForUser(userName, modulName, objectId, permissionName);
            return Ok(response);
        }

        [HttpGet("checkRbacPerUserPerObjectByOjectType")]
        public IActionResult checkRbacPerUserPerObjectByOjectType(string userName, string modulName, string objectId, string permissionName, string objectType)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            var response = e.HasPermissionForUser(userName, modulName, objectId, permissionName, objectType);
            return Ok(response);
        }

        [HttpPost("addPermissionWithoutDomain")]
        public IActionResult addPermissionWithoutDomain(string userName, string modulName, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.AddPolicy(userName, modulName, permissionName);
            return Ok(response);
        }

        [HttpPost("addPermissionWithDomain")]
        public IActionResult addPermissionWithDomain(string userName, string modulName, string objectId, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.AddPolicy(userName, modulName, objectId, permissionName);
            return Ok(response);
        }

        [HttpPost("addPermissionPerObjectByObjectType")]
        public IActionResult addPermissionPerObjectByObjectType(string userName, string modulName, string objectId, string permissionName, string objectType)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.AddPolicy(userName, modulName, objectId, permissionName, objectType);
            return Ok(response);
        }

        [HttpPost("addGeneralRoleWithoutDomain")]
        public IActionResult addGeneralRoleWithoutModel(string userName, string roleName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.AddRoleForUser(userName, roleName);
            return Ok(response);
        }

        [HttpPost("addGeneralRoleWithDomain")]
        public IActionResult addGeneralRoleWithDomain(string userName, string modulName, string roleName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.AddRoleForUserInDomain(userName, roleName, modulName);
            return Ok(response);
        }

        [HttpDelete("deleteRoleWithoutDomain")]
        public IActionResult deleteRoleWithoutDomain(string userName, string roleName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.DeleteRoleForUser(userName, roleName);
            return Ok(response);
        }

        [HttpDelete("deletePermissionWithoutObject")]
        public IActionResult deletePermissionWithoutObject(string userName, string modulName, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.DeleteRoleForUserInDomain(userName, modulName, permissionName);
            return Ok(response);
        }

        [HttpDelete("deletePermissionWithObject")]
        public IActionResult deletePermissionWithObject(string userName, string modulName, string objectId, string permissionName)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            bool response = e.DeletePermissionForUser(userName, modulName, objectId, permissionName);
            return Ok(response);
        }

        [HttpGet("objectsByUser/{user}")]
        public IActionResult objectsByUser(string user, string domain)
        {
            var efAdapter = new CasbinDbAdapter<int>(_context);
            Enforcer e = new Enforcer("CasbinConfig/rbac_model.conf", efAdapter);
            var userPersmissions = e.GetPermissionsForUser(user);
            var permissionsByDomain = e.GetPermissionsForUserInDomain(user, domain);
            //var userObjects = _context.CasbinRule.Where(m => m.V0 == user);
            //  var test = _context.CasbinRule.Where(m => m.V0 == user && m.V1 == domain);
            return Ok(userPersmissions);
        }
    }
}