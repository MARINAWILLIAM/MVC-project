using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.Pl.Helpers;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    [Authorize]
	public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IunitOfWork _unitofwork;

        public EmployeeController(/*IEmployeeRepository EmployeeRepository,
                                   * IDepartmentRepository departmentRepository,
           */ IMapper mapper, IunitOfWork unitofwork)
        {
            //_employeeRepository = EmployeeRepository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
           _unitofwork = unitofwork;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            // ViewData["Message"] = "Hello View Data";
            //ViewBag.Message = "Hello View Bag";
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employees = await _unitofwork.employeeRepository.GetAll();
                
            }
            else
            {
                employees= _unitofwork.employeeRepository.SearchEmployeeByAddress(SearchValue);
              
            }
            var mapedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
           
            return View(mapedemp);




        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Department = await _unitofwork.departmentRepository.GetAll();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                #region MANUALL MAPPING
                ////
                //var Employee=new Employee()
                //{
                //    Name = employeeViewModel.Name,
                //    Address = employeeViewModel.Address,
                //    Email = employeeViewModel.Email,
                //    Salary = employeeViewModel.Salary,
                //    Age = employeeViewModel.Age,
                //    DepartmentsId = employeeViewModel.DepartmentsId,
                //    ISACTIVE = employeeViewModel.ISACTIVE,
                //    HireDate = employeeViewModel.HireDate,
                //    PhoneNumber = employeeViewModel.PhoneNumber,
                //}; 
                #endregion
                //  Employee employee = (Employee)employeeViewModel;
                employeeViewModel.ImageName = DocumentSetting.Uploadfile(employeeViewModel.Image, "Images");

                var mapemp =_mapper.Map<EmployeeViewModel,Employee>(employeeViewModel);
                await  _unitofwork.employeeRepository.Add(mapemp);
             //int count= await  _unitofwork.Complete();//number row hasleha effect
             //   if (count > 0)
             //   {
             //       TempData["Message"] = "Employee is Created Successfully";
             //   }
            await _unitofwork.Complete();
                
               
                
                return RedirectToAction(nameof(Index));


            }

            return View(employeeViewModel);
        }
        public  async Task<IActionResult> Details(int? id, string ViewName = "Details")

        {
            if (id == null)

                return BadRequest();

            var employee = await _unitofwork.employeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();
            var mapedemp=_mapper.Map<Employee,EmployeeViewModel>(employee);
            return View(ViewName, mapedemp);
        }
        public async Task<IActionResult> Edit(int? id)

        {
            // ViewBag.Department = _departmentRepository.GetAll();

            
            return await Details(id, "Edit");
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeViewModel)

        {
            if (id != employeeViewModel.Id)
            

                return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {
                    employeeViewModel.ImageName = DocumentSetting.Uploadfile(employeeViewModel.Image, "Images");
                    var mapempp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);
                    _unitofwork.employeeRepository.Update(mapempp);
                    await _unitofwork.Complete();
                    return RedirectToAction(nameof(Index));



                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);
                }
            }
                await _unitofwork.Complete();

                return View(employeeViewModel);
            
            
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeViewModel)
        {
           
            if (id != employeeViewModel.Id) { return BadRequest(); }
            try
            {
                var mapempp = _mapper.Map<EmployeeViewModel, Employee>(employeeViewModel);


                _unitofwork.employeeRepository.Delete(mapempp);

            int count=await _unitofwork.Complete();
                if (count > 0) 
                
                    DocumentSetting.Delete(mapempp.ImageName, "Images");
                
                return RedirectToAction(nameof(Index));
                

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeViewModel);
            }
            
        }







    }
}
