using Microsoft.EntityFrameworkCore;
using OSlBackendWebService.Interfaces;
using OSlBackendWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSlBackendWebService.Services
{
    public class OslRepository:IOSLRepository
    {
        private List<ToDoItems> _toDoList;

        private readonly OslMobileBackendWebserviceContext _ctx;

        public OslRepository(OslMobileBackendWebserviceContext ctx)
        {
            InitializeData();
            _ctx = ctx;
        }

        public IEnumerable<ToDoItems> All
        {
            get { return _toDoList; }
        }

        public IEnumerable<Employees> GetAll
        {
            get { return _ctx.Employees.ToList(); }
        }


        public bool DoesItemExist(string id)
        {
            return _toDoList.Any(item => item.ID == id);
        }

        public ToDoItems Find(string id)
        {
            return _toDoList.FirstOrDefault(item => item.ID == id);
        }

        public void Insert(ToDoItems item)
        {
            _toDoList.Add(item);
        }

        public void Update(ToDoItems item)
        {
            var todoItem = this.Find(item.ID);
            var index = _toDoList.IndexOf(todoItem);
            _toDoList.RemoveAt(index);
            _toDoList.Insert(index, item);
        }

        public void Delete(string id)
        {
            _toDoList.Remove(this.Find(id));
        }

        private void InitializeData()
        {
            _toDoList = new List<ToDoItems>();

            var todoItem1 = new ToDoItems
            {
                ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "Learn app development",
                Notes = "Attend Xamarin University",
                Done = true
            };

            var todoItem2 = new ToDoItems
            {
                ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "Develop apps",
                Notes = "Use Xamarin Studio/Visual Studio",
                Done = false
            };

            var todoItem3 = new ToDoItems
            {
                ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "Publish apps",
                Notes = "All app stores",
                Done = false,
            };

            _toDoList.Add(todoItem1);
            _toDoList.Add(todoItem2);
            _toDoList.Add(todoItem3);
        }

        public IQueryable<Employees> GetAllEmployees()
        {
            return _ctx.Employees.AsQueryable();
        }

        public Employees GetEmployee(int EmpID)
        {
            var employee = _ctx.Employees
                .Include(a=>a.Station)
                .Where(s => s.EmpId == EmpID)
                .SingleOrDefault();
            return employee;
        }

        public bool DoesEmpExist(int EmpID)
        {
           
            return _ctx.Employees.Any(s => s.EmpId == EmpID);
        }

        public bool LoginEmp(int EmpID, int password)
        {
            var employee = _ctx.Employees.Where(a => a.EmpId == EmpID).SingleOrDefault();
            if(employee != null)
            {
                if(employee.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(Employees emp)
        {
             _ctx.Employees.Add(emp);
        }

        public void Update(Employees originalemp, Employees UpdatedEmp)
        {
            _ctx.Entry(originalemp).CurrentValues.SetValues(UpdatedEmp);
            
        }

        public void DeleteEmp(int EmpID)
        {
            _ctx.Remove(this.GetEmployee(EmpID));

        }

        public IQueryable<Stations> GetAllStations()
        {
            return _ctx.Stations.AsQueryable();
        }

        public Stations GetStations(int StationID)
        {
            var stations = _ctx.Stations
                .Include(a => a.Employees)
                .Where(a => a.Stid == StationID)
                .SingleOrDefault();
            return stations;
        }

        public bool DoesStationExist(int stationId)
        {
            return _ctx.Stations.Any(c => c.Stid == stationId);
        }

        public void Insert(Stations stn)
        {
            _ctx.Stations.Add(stn);
        }

        public void Update(Stations Original, Stations updated)
        {
            _ctx.Entry(Original).CurrentValues.SetValues(updated);
        }

        public void DeleteStn(int stationId)
        {
            _ctx.Stations.Remove(this.GetStations(stationId));
        }

        public IQueryable<Supervisors> GetAllSupervisors()
        {
            return _ctx.Supervisors.AsQueryable();

        }

        public Supervisors GetSup(int SupID)
        {
            var supervisor = _ctx.Supervisors
                .Include(x => x.Employees)
                .Where(x => x.SupId == SupID)
                .SingleOrDefault();
            return supervisor;
        }

        public bool DoesSupExist(int SupID)
        {
            return _ctx.Supervisors.Any(se => se.SupId == SupID);
        }

        public bool LoginSup(int SupID, int Password)
        {
            var supervisor = _ctx.Supervisors.Where(a => a.SupId == SupID).SingleOrDefault();
            if (supervisor != null)
            {
                if (supervisor.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public void Insert(Supervisors supervisor)
        {
            _ctx.Supervisors.Add(supervisor);
        }

        public void Update(Supervisors supervisorold, Supervisors newSup)
        {
            _ctx.Entry(supervisorold).CurrentValues.SetValues(newSup);
        }

        public void Deletesup(int SupID)
        {
            _ctx.Supervisors.Remove(this.GetSup(SupID));
        }

        public void Insert(EmployeesLogs Emplogs)
        {
            _ctx.EmployeesLogs.Add(Emplogs);
        }

        public void Insert(Checkings checks)
        {
            _ctx.Checkings.Add(checks);
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
