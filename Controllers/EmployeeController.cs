using ASP_Dapper.Models;
using ASP_Dapper.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASP_Dapper.Controllers
{
    public class EmployeeController: Controller
    {
        #region Ctor
        private readonly ICompanyRepository _companyRepository; 
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBonusRepository _bankRepository;

        public EmployeeController(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IBonusRepository bankRepository)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _bankRepository = bankRepository;

        }
        #endregion

        #region Bind Properties
        [BindProperty]
        public Employee Employee { get; set; }
        #endregion

        // GET: Companies
        public async Task<IActionResult> Index(int id = 0)
        {
            //Select * From Companies
            //return View(_employeeRepository.GetAll());
            //List<Employee> employees = _employeeRepository.GetAll();
            //foreach (var item in employees)
            //{
            //    item.Company = _companyRepository.GetCompany(item.CompanyId);
            //}
            return View(_bankRepository.GetEmployeesWithCompany(id));
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            //fill dropdown
            ViewBag.CompanyId = new SelectList(_companyRepository.GetAll(), "CompanyId", "Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.AddEmployee(Employee);
                return RedirectToAction(nameof(Index));
            }
            //fill dropdown
            ViewBag.CompanyId = new SelectList(_companyRepository.GetAll(), "CompanyId", "Name");
            return View(Employee);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Employee = _employeeRepository.GetEmployee(id.Value);

            //fill dropdown
            ViewBag.CompanyId = new SelectList(_companyRepository.GetAll(), "CompanyId", "Name", Employee.CompanyId);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepository.UpdateEmployee(Employee);
                   
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            //fill dropdown
            ViewBag.CompanyId = new SelectList(_companyRepository.GetAll(), "CompanyId", "Name", Employee.CompanyId);
            return View(Employee);
        }


        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = _employeeRepository.GetEmployee(id.Value);
            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);
        }





        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = _employeeRepository.GetEmployee(id.Value);
            if (Employee == null)
            {
                return NotFound();
            }

            return View(Employee);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
