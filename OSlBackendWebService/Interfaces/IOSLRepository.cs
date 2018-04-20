using OSlBackendWebService.Models;
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
        Employees GetEmployee(int EmpID);
        bool DoesEmpExist(int EmpID);
        bool LoginEmp(int EmpID, int password);
        void Insert(Employees emp);
        void Update(Employees originalemp,Employees UpdatedEmp);
        void DeleteEmp(int empID);

        IQueryable<EmployeesLogs> GetLogsByDate(DateTime date);

        IQueryable<Stations> GetAllStations();
        Stations GetStations(int StationID);
        bool DoesStationExist(int stationId);
        void Insert(Stations stn);
        void Update(Stations Original, Stations updated);
        void DeleteStn(int stationId);

        IQueryable<Supervisors> GetAllSupervisors();
        Supervisors GetSup(int SupID);
        bool DoesSupExist(int SupID);
        bool LoginSup(int SupID, int Password);
        void Insert(Supervisors supervisor);
        void Update(Supervisors supervisorold, Supervisors newSup);
        void Deletesup(int SupID);

        void Insert(EmployeesLogs Emplogs);
        void Insert(Checkings checks);
        bool SaveAll();

    }
}
