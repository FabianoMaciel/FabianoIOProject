using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementStudents.Domain
{
    public class User : Entity, IAggregateRoot
    {
        public string UserName { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        private readonly List<Registration> registrations = [];
        public IReadOnlyCollection<Registration> Registrations => registrations;
         
        //TO DO Manipulação: Entidade Aluno gerencia diretamente suas Matrículas e 
        //Certificados.
    }
}
