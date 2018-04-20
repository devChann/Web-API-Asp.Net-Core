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
        private readonly OslMobileBackendWebserviceContext _ctx;

        public OslRepository(OslMobileBackendWebserviceContext ctx)
        {
            
            _ctx = ctx;
        }

       
        public IEnumerable<Employees> GetAll
        {
            get { return _ctx.Employees.ToList(); }
        }

       
        public Employees Find(int id)
        {
            return _ctx.Employees.FirstOrDefault(sa => sa.EmpId == id);
     
        }

       

        public IQueryable<Employees> GetAllEmployees()
        {
            return _ctx.Employees.AsQueryable();
        }

        public Employees GetEmployee(int EmpID)
        {
            var employee = _ctx.Employees
                //.Include(a=>a.Station)
                .Where(s => s.EmpId == EmpID)
                .SingleOrDefault();
            return employee;
        }

        
        public IQueryable<EmployeesLogs> GetLogsByDate(DateTime date)
        {
            return _ctx.EmployeesLogs
                        .Include(sa=>sa.Emp).ThenInclude(a=>a.StationId)
                        
                    .Where(c => c.Qdate == date)
                    .AsQueryable();
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
                //.Include(a => a.Employees)
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

       

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
