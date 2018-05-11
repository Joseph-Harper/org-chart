using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrgChart.Core.Entities;
using OrgChart.Core.Interfaces;
using OrgChart.Core.Specifications;
using OrgChart.Infrastructure.Identity;
using OrgChart.Web.ViewModels.Organization;
using System.Linq;
using System.Threading.Tasks;

namespace OrgChart.Web.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IAuthorizationService _authorizationService;

        public OrganizationController(
            UserManager<ApplicationUser> userManager,
            IRepository<Organization> organizationRepository,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _organizationRepository = organizationRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var specification = new OrganizationWithPeopleSpecification();
            var viewModel = _organizationRepository
                .List(specification)
                .Select(o => new OrganizationViewModel(o));

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddOrganizationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var organization = new Organization
            {
                UserId = _userManager.GetUserId(User),
                Name = model.Name
            };

            _organizationRepository.Add(organization);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddPerson(int organizationId)
        {
            var specification = new OrganizationWithPeopleSpecification(organizationId);
            var organization = _organizationRepository.GetBySpecification(specification);
            if (organization == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, organization, Operations.Read);

            if (!authorizationResult.Succeeded) return Forbid();
            var viewModel = new AddPersonViewModel(organization);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(AddPersonViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var specification = new OrganizationWithPeopleSpecification(model.OrganizationId);
            var organization = _organizationRepository.GetBySpecification(specification);

            if (organization == null) return BadRequest();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, organization, Operations.Update);

            if (!authorizationResult.Succeeded) return Forbid();

            organization.AddPerson(model.FirstName, model.LastName, model.EmailAddress,
                model.PhoneNumber, model.Title, model.ReportsToPersonID);

            _organizationRepository.Update(organization);

            return RedirectToAction(nameof(People), new { organizationId = organization.Id });
        }

        [HttpGet]
        public async Task<IActionResult> People(int organizationId)
        {
            var specification = new OrganizationWithPeopleSpecification(organizationId);
            var organization = _organizationRepository.GetBySpecification(specification);

            if (organization == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, organization, Operations.Read);

            if (!authorizationResult.Succeeded) return Forbid();

            var viewModel = new PeopleViewModel();
            viewModel.OrganizationId = organization.Id;
            viewModel.People = organization.People.Select(p => new PersonViewModel(p));

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Chart(int organizationId)
        {
            var specification = new OrganizationWithPeopleSpecification(organizationId);
            var organization = _organizationRepository.GetBySpecification(specification);

            if (organization == null) return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, organization, Operations.Read);

            if (!authorizationResult.Succeeded) return Forbid();

            var people = organization.People
                .Select(p => new
                {
                    Id = p.Id.ToString(),
                    Name = (p.FirstName + " " + p.LastName).Trim(),
                    p.Title,
                    ReportsToId = p.ReportsTo?.Id.ToString() ?? ""
                });

            var viewModel = new ChartViewModel { PeopleJson = JsonConvert.SerializeObject(people) };
            return View(viewModel);
        }
    }
}