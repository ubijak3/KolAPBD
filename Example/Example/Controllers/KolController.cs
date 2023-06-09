﻿using Example.DAL;
using Example.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KolController : ControllerBase
    {

        private readonly _2019sbdContext _context;
        public KolController(_2019sbdContext _2019SbdContext) {
            _context = _2019SbdContext;
        }

        [HttpGet]
        public IActionResult Get(string? surname)
        {

            if (surname == null){
                /*var prescriptions = _context.Prescriptions
                                .OrderByDescending(p => p.Date)
                                .Select(p => new
                                {
                                    IdPerscription = p.IdPrescription,
                                    Date = p.Date,
                                    dueDate = p.DueDate,
                                    DoctorLastName = p.IdPatientNavigation.LastName,
                                    PatientLastName = p.IdPatientNavigation.LastName
                                })
                                .ToList();*/
                var prescription = from p in _context.Prescriptions
                                   from d in _context.Doctors
                                   from pat in _context.Patients
                                   where p.IdPatient == pat.IdPatient && p.IdDoctor == d.IdDoctor
                                   orderby p.Date descending
                                   select new
                                   {
                                       IdPerscription = p.IdPrescription,
                                       Date = p.Date,
                                       dueDate = p.DueDate,
                                       DoctorLastName = d.LastName,
                                       PatientLastName = pat.LastName
                                   };
                return Ok(prescription);
            }

            /*var prescriptions = _context.Prescriptions
                                .Where(p => p.IdDoctorNavigation.LastName == surname)
                                .OrderByDescending(p => p.Date)
                                .Select(p => new
                                {
                                    IdPerscription = p.IdPrescription,
                                    Date = p.Date,
                                    dueDate = p.DueDate,
                                    DoctorLastName = p.IdPatientNavigation.LastName,
                                    PatientLastName = p.IdPatientNavigation.LastName
                                });*/
            /*var prescriptions = _context.Prescriptions
                            .Include(p => p.IdDoctorNavigation)
                            .Include(p => p.IdPatientNavigation)
                            .Where(p => p.IdDoctorNavigation.LastName == surname)
                            .OrderByDescending(p => p.Date)
                                .Select(p => new
                                {
                                    IdPerscription = p.IdPrescription,
                                    Date = p.Date,
                                    dueDate = p.DueDate,
                                    DoctorLastName = p.IdPatientNavigation.LastName,
                                    PatientLastName = p.IdPatientNavigation.LastName
                                });*/
            var prescriptions = from p in _context.Prescriptions
                               from d in _context.Doctors
                               from pat in _context.Patients
                               where p.IdPatient == pat.IdPatient && p.IdDoctor == d.IdDoctor && d.LastName == surname
                               orderby p.Date descending
                               select new
                               {
                                   IdPerscription = p.IdPrescription,
                                   Date = p.Date,
                                   dueDate = p.DueDate,
                                   DoctorLastName = d.LastName,
                                   PatientLastName = pat.LastName
                               };

            return Ok(prescriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionPOST prescriptionPOST)
        {

            if(prescriptionPOST.DueDate < prescriptionPOST.Date)
            {
                return BadRequest("DueDate cannot be bigger than Date");
            }
            var pre = new Prescription
            {
                Date = prescriptionPOST.Date,
                DueDate = prescriptionPOST.DueDate,
                IdPatient = prescriptionPOST.IdPatient,
                IdDoctor = prescriptionPOST.IdDoctor
            };

            await _context.AddAsync(pre);
            await _context.SaveChangesAsync();

            return Ok(pre);    
        }
    }
}
