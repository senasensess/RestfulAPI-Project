using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI_Project.Infrastructure.Repositories.Interfaces;
using RestfulAPI_Project.Models.DTO_s.CategoryDTO;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Controllers
{
    /*
     * ProducesResponseTypes=> Bir action methodu içerisinde bir çok dönüş türü ve yolu bulunma ihtimali yüksektir. "ProducesResponseTypes" özniteliği kullanılarak faklı dönüş tiplerini Swagger gibi araçlara tarafında dökümantasyonlarında istemciler için daha açıklayıcı yanıt ayrıntıları üretir. 
     */

    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(400)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //Swagger UI aracılığyla API üzerinde bazı testler yapmak isteyen geliştiriciler için bazı özet bilgiler ekliyoruz ki ilgili geliştirici API'yi rahatlıkla test etsin. Yani API'nin yetenekleri hakkında çıklama yapıyoruz. İlgili Action methodun ne parametre aldığı ne iş yaptığı vb.

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GetCategoryDTO>))]
        public IActionResult GetCategory()
        {
            var categories = _categoryRepository.GetCategories();
            var model = new List<GetCategoryDTO>();
            foreach (var category in categories)
            {
                var categoryModel = _mapper.Map<GetCategoryDTO>(category);
                model.Add(categoryModel);
            }

            return Ok(model);
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">The Id of Category</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get Category")]
        [ProducesResponseType(200), ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var model = _mapper.Map<GetCategoryDTO>(category);
                return Ok(model);
            }
        }

        //[HttpGet]
        //[ProducesResponseType(200), ProducesResponseType(400)]
        //public IActionResult GetCategory([FromQuery]int id)
        //{
        //    var category = _categoryRepository.GetCategory(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        var model = _mapper.Map<GetCategoryDTO>(category);
        //        return Ok(model);
        //    }
        //}


        /*
         * Action Methodlar
         * 
         * 1) FromBody 
         *  HTTP Request'inin body'si içerisinde gönderilen parametreleri okumak için kullanılır.
         *  
         * 2) FromQuery
         *  URL içerisinde gömülen parametreleri okumak için kullanılan attribute'dür.
         *  
         * 3) FromRoute
         *  Endpoint url'i içerisinde gönderilern parametreleri okumak için kullanılır. Yaygın olarak resource'a ait id bilgisi okurken kullanılır.
         */


        /// <summary>
        /// Add The New Category
        /// </summary>
        /// <param name="model">In this process, Name and Description does requiert fields!!</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody]CreateCategoryDTO model)
        {
            if (model == null)
            {
                return StatusCode(404, ModelState);
            }

            if (_categoryRepository.CategoryExists(model.Name))
            {
                ModelState.AddModelError("", "Bu kategori ismi zaten kullanılıyor!");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(model);
            var result = _categoryRepository.CategoryCreate(category);

            if (!result)
            {
                ModelState.AddModelError("", $"Bir şeyler ters gitti!\nKategori adı => {category.Name}\nAçıklaması: {category.Description}");
                return StatusCode(500, ModelState);
            }

            return Ok(category);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="model">In this process, Id, Name and Description does requiert fields</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] UpdateCategoryDTO model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(model);
            var result = _categoryRepository.CategoryUpdate(category);
            if (!result)
            {
                ModelState.AddModelError("", "Birşeyler ters gitti!!");
                return StatusCode(500, ModelState);
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound(ModelState);
            }
            var result = _categoryRepository.CategoryDelete(id);
            if (!result)
            {
                ModelState.AddModelError("", "Birşeyler ters gitti!!");
            }
            return Ok("Kategori silindi!");
        }
    }
}
