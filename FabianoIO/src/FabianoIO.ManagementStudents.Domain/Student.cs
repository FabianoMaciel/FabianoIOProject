using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementStudents.Domain
{
    public class Student : Entity, IAggregateRoot
    {
        private readonly List<Registration> registrations = [];
        public IReadOnlyCollection<Registration> Registrations => registrations;
         
        //TO DO Manipulação: Entidade Aluno gerencia diretamente suas Matrículas e 
        //Certificados.
        //tt
    }
}
