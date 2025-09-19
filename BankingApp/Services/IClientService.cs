using BankingApp.DTOs;
using BankingApp.Models;

namespace BankingApp.Services
{
    public interface IClientService
    {
        IEnumerable<ClientDto> GetAll();
        ClientDto? GetById(int id);
        ClientDto Add(CreateClientDto dto);
        ClientDto? Update(int id, UpdateClientDto dto);
        bool Delete(int id);
    }
}
