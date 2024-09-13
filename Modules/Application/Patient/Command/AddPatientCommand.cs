using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using MediatR;

namespace CareTrack.Server.Modules.Application.Patient.Command;

public class AddPatientCommand : IRequest<Result<IPatient>>
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public int Weight { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Admission { get; set; }
    public DateTime Discharge { get; set; }
}

public class AddPatientCommandHandler
    : IRequestHandler<AddPatientCommand, Result<IPatient>>
{
    private readonly IPatientRepository patientRepository;
    public AddPatientCommandHandler(IPatientRepository patientRepository)
    {
        this.patientRepository = patientRepository;
    }
    public async Task<Result<IPatient>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new PatientResult
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Weight = request.Weight,
            PhoneNumber = request.PhoneNumber,
            Admission = request.Admission,
            Discharge = request.Discharge
        };
        var patientResult = await patientRepository.Add(patient);
        return patientResult;
    }
}