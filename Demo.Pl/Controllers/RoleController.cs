using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Demo.Pl.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,IMapper mapper
            ,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }
       
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                   
                   Id = R.Id,
                   RoleName=R.Name

                }).ToListAsync();

                return View(roles);

            }
           
            else
            {
                var role = await _roleManager.FindByNameAsync(name);

                if (role is not null)
                {
                    var mappedRole = new RoleViewModel
                    {
                        Id = role?.Id,
                        RoleName = role.Name


                    };

                    return View(new List<RoleViewModel>() { mappedRole });
                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)//hygli eh?
        {
            if(ModelState.IsValid)
            {
                var mappedrole=  _mapper.Map<RoleViewModel,IdentityRole>(roleViewModel);//maped elawl
                var result= await _roleManager.CreateAsync(mappedrole);
                if(result.Succeeded)
                
                    return RedirectToAction("Index");

                foreach (var Error in result.Errors)

                    ModelState.AddModelError(string.Empty, Error.Description);
                  
            
            }
            return View(roleViewModel);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")

        {
            if (id == null)

                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            return View(ViewName, mappedrole);
        }

        public async Task<IActionResult> Edit(string id)
           {
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleViewModel)

        {
            if (id != roleViewModel.Id)


                return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roleViewModel.RoleName;
                   
                    await _roleManager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));



                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }
            }


            return View(roleViewModel);


        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if(id is null)
            {
                return NotFound();
            }

            try
            {
                var role = await _roleManager.FindByIdAsync(id);

                var resule=await _roleManager.DeleteAsync(role);

                if(resule.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var item in resule.Errors)
                    ModelState.AddModelError(string.Empty, item.Description);

                

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task< IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if(role == null)
            {
                return BadRequest();
            }
            ViewBag.RoleId = RoleId;
            var users = new List<RoleUserViewModel>();
            foreach(var user in _userManager.Users)
            {
                var UserInRole = new RoleUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if(await _userManager.IsInRoleAsync(user,role.Name))
                    UserInRole.IsSelected = true;
                else
                    UserInRole.IsSelected = false;
                users.Add(UserInRole);
            }
            return View(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<RoleUserViewModel> models,string RoleId)
        {
var role =await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var item in models)
                {
                    var user = await _userManager.FindByIdAsync(item.UserId);
                    if (user is not null)
                    {
                        if(item.IsSelected &&!(await _userManager.IsInRoleAsync(user,role.Name)))
                        await _userManager.AddToRoleAsync(user,role.Name);
                        else if(!item.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);

                    }
                }
                return RedirectToAction("Edit",new {id=RoleId});
            }

            return View(models);














        }
    }
}
