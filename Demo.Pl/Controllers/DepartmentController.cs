using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;

        ///public IDepartmentRepository DepartmentRepository { get; }

        // private IDepartmentRepository _DepartmentRepository;
        public DepartmentController(/*IDepartmentRepository departmentRepository*/ IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_DepartmentRepository = departmentRepository;
            // _DepartmentRepository =new DepartmentController();
            // _DepartmentRepository = departmentRepository;

        }

      
        public async Task<IActionResult> Index(string SearchValue)
        {
          
            if (string.IsNullOrEmpty(SearchValue))
            {
               var departments = await  _unitOfWork.departmentRepository.GetAll();
                return View(departments);
            }
            else
            {
              var  departments =  _unitOfWork.departmentRepository.SearchByDepartmentByAddress(SearchValue);
                return View(departments);
            }


            //if (string.IsNullOrEmpty(SearchValue))
            //{
            //    employees = _employeeRepository.GetAll();

            //}
            //else
            //{
            //    employees = _employeeRepository.SearchEmployeeByAddress(SearchValue);

            //}
            //var mapedemp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            //return View(mapedemp);




        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Departments department)
        {
            if (ModelState.IsValid)
            { 
       await  _unitOfWork.departmentRepository.Add(department);
                int count = await _unitOfWork.Complete();
                if (count>0)
                {
                    TempData["Message"] = "Department is Created Successfully";
                }
                //TempData
                return RedirectToAction(nameof(Index));
            
            
            }

            return View(department);
        }
        public async Task< IActionResult> Details(int? id,string ViewName= "Details")

        {
            if (id == null)

                return BadRequest();
            var department = await _unitOfWork.departmentRepository.Get(id.Value);
            if (department is null)
                return NotFound();

            //protectyive code 
            return View(ViewName,department);
        }
        public async Task<IActionResult>Edit(int? id)

        {
            return await Details(id, "Edit");
            //if (id == null)

            //    return BadRequest();
            //var department = _DepartmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound();

            ////protectyive code 
            //return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,Departments department)

        {
            if(id != department.Id)
            
                return BadRequest();



            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.departmentRepository.Update(department);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));


                }

                catch (Exception ex)
                {
                    //law sh8al fe el dvelopment
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
      
        public async Task< IActionResult> Delete (int? id)
        {
            return await Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id,Departments departments)
        {
            if (id != departments.Id) { return BadRequest(); }
            try
            {
                _unitOfWork.departmentRepository.Delete(departments);
                    await  _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {

               //log expection
               //friendly message
               ModelState.AddModelError(string.Empty,ex.Message);
                return View(departments);
            }

        }













    }
}
