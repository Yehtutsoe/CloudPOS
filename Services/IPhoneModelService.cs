using CloudPOS.Models.ViewModels;

namespace CloudPOS.Services
{
    public interface IPhoneModelService
    {
        void Create(PhoneModelViewModel modelViewModel);
        IEnumerable<PhoneModelViewModel> RetrieveAll();
        PhoneModelViewModel GetById(string Id);
        void Update(PhoneModelViewModel modelViewModel);
        void Delete(string Id);
    }
}
