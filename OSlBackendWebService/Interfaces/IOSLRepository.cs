using GeoJSON.Net.Feature;
using OSlBackendWebService.Models;
using OSlBackendWebService.oslviewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSlBackendWebService.Interfaces
{
    public interface IOSLRepository
    {
       
       
        void Delete(string id);



        IQueryable<Employees> GetAllEmployees();
        IEnumerable<Employees> GetAll { get; }
        FeatureCollection GetAllEmployeeLogs();
        EmployeelistViewModel GetEmployee(int EmpID);
        bool DoesEmpExist(int EmpID);
        bool LoginEmp(int EmpID, int password);
        void Insert(Employees emp);
        void Update(Employees originalemp,Employees UpdatedEmp);
        void DeleteEmp(int empID);
        //Task<List<EmployeesLogsViewModel>> GetLogsForStation();

        Emplogs Logs(int id);
        bool isLogExist(int empid);
        void UpdateEmployee(EmployeesLogs checkedstatus);
        EmployeesLogs find(int id);
        IQueryable<Stations> GetAllStations();
        EmployeesLogsViewModel GetStations(string Name);
        bool DoesStationExist(int stationId);
        void Insert(Stations stn);
        void Update(Stations Original, Stations updated);
        //void DeleteStn(int stationId);

        IQueryable<Supervisors> GetAllSupervisors();
        Supervisors GetSup(int SupID);
        bool DoesSupExist(int SupID);
        bool LoginSup(int SupID, int Password);
        void Insert(Supervisors supervisor);
        void Update(Supervisors supervisorold, Supervisors newSup);
        void Deletesup(int SupID);

        void Insert(EmployeesLogs Emplogs);
        void Insert(Checkings checks);
        bool CheckSupervisorLogs(int supID);
        void SaveAll();

    }
}
