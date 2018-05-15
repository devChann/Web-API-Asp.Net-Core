using Microsoft.EntityFrameworkCore;
using OSlBackendWebService.Interfaces;
using OSlBackendWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OSlBackendWebService.oslviewmodels;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System.Spatial;
using GeoJSON.Net.Feature;

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
        public FeatureCollection GetAllEmployeeLogs()
        {


            var emplogs = _ctx.EmployeesLogs
                           .ToList();
            List<LatLongViewModel> latlong = new List<LatLongViewModel>();
            List<LogsViewModel> logsviewmodel = new List<LogsViewModel>();
            List<Feature> lstGeoLocation = new List<Feature>();
            var model = new FeatureCollection() { CRS = new GeoJSON.Net.CoordinateReferenceSystem.NamedCRS("urn:ogc:def:crs:OGC::CRS84") };
            emplogs.ForEach(x =>
            {
                LatLongViewModel Obj = new LatLongViewModel();
                LogsViewModel logs = new LogsViewModel();
                logs.EmpId = x.EmpId;
                logs.Lit = x.Lit;
                logs.Lot = x.Lot;
                logs.TranszactionId = x.TranszactionId;
                logs.CheckedSatus = x.CheckedSatus;
                Obj.Coordinates = new Point(new Position(x.Xcoord, x.Ycoord));
                Feature feature = new Feature(new Point(new Position(x.Xcoord,x.Ycoord)), logs);

                lstGeoLocation.Add(feature);
                model.Features.Add(feature);
               
            });
            //var actualJson = JsonConvert.SerializeObject(model);
            return model;
        }

        public Employees Find(int id)
        {
            return _ctx.Employees.FirstOrDefault(sa => sa.EmpId == id);
     
        }

       

        public IQueryable<Employees> GetAllEmployees()
        {
            return _ctx.Employees.AsQueryable();
        }

        public EmployeelistViewModel GetEmployee(int EmpID)
        {
            var employee = _ctx.Employees
               
                .Where(s => s.EmpId == EmpID)
                .SingleOrDefault();
            var viewmodel = new EmployeelistViewModel();
            viewmodel.EmpId = employee.EmpId;
            viewmodel.Password = employee.Password;
            return viewmodel;
        }      

        public bool DoesEmpExist(int EmpID)
        {
           
            return _ctx.Employees.Any(s => s.EmpId == EmpID);
        }
        public bool isLogExist(int empid)
        {
            String CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");

            return _ctx.EmployeesLogs.Any(s => s.EmpId == empid & s.StringDate ==CurrentDate);
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
        public void Insert(Checkings checkings)
        {
            _ctx.Checkings.Add(checkings);
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

        public EmployeesLogsViewModel GetStations(string name)
        {
            var stations = _ctx.Stations
                .Include(a => a.Employees)
               
                .Where(a => a.StationName == name)
                .SingleOrDefault();
            var test = new EmployeesLogsViewModel();
            test.StationName = stations.StationName;
            test.employees = stations.Employees;
            return test;
        }
       
        public Emplogs Logs(int id)
        {

            String CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            var viewmodel = new Emplogs();
            var logdata = _ctx.EmployeesLogs
                .Where(sa => sa.EmpId == id & sa.StringDate == CurrentDate)
                .SingleOrDefault();
            viewmodel.Lit = logdata.Lit;
            viewmodel.CheckedSatus = logdata.CheckedSatus;
            return viewmodel;
                
              
                                    
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

        //public void Insert(Checkings checks)
        //{
        //    _ctx.Checkings.Add(checks);
        //}
        public void SaveAll()
        {
            _ctx.SaveChanges();
        }

       

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        public void UpdateEmployee(EmployeesLogs checkedstatus)
        {
            if (checkedstatus == null)
                throw new ArgumentNullException(nameof(checkedstatus));
            _ctx.EmployeesLogs.Update(checkedstatus);
        }

        public EmployeesLogs find(int id)
        {
            String CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            
            var logdata = _ctx.EmployeesLogs
                .Where(sa => sa.EmpId == id & sa.StringDate == CurrentDate)
                .SingleOrDefault();
            
            return logdata;
        }

        public bool CheckSupervisorLogs(int supID)
        {
            String CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");

            return _ctx.Checkings.Any(s => s.SupId == supID & s.StringDate == CurrentDate);
        }
    }
}
