
using AutoMapper;
using Liwapoi.Api.Common;
using Liwapoi.Api.Models.RequestModel;
using Liwapoi.Api.Models.ResponseModel;
using Liwapoi.BLL.Services.Interfaces;
using Liwapoi.Models.BLL;
using Liwapoi.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Liwapoi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Private fields
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public UserController(IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;

        }
        #endregion

        #region Endpoints
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenModel>> Login(LoginRequest user)
        {
            if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception(Errors.INVALID_USER_MODEL);
            }

            var loginModel = _mapper.Map<LoginModel>(user);

            if (await _userService.IsValid(loginModel))
            {
                var currentUser = await _userService.Get(user.Name);

                var token = await _tokenService.CreateToken(user.Name, currentUser.Id);

                return Ok(token);
            }

            throw new Exception(Errors.INVALID_USER_MODEL);
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value", "value2"};
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<ActionResult<string>> Upload()
        {
            if (Request.Form.Files.Count == 0)
            {
                throw new Exception(Errors.FILE_UPLOAD_ERROR);
            }

            var imageUploader = new ImageUploader();

            var file = Request.Form.Files[0];
            var uploadFileName = await imageUploader.Upload(file);

            if (!string.IsNullOrEmpty(uploadFileName))
            {
                return Ok(new { uploadFileName });
            }

            return BadRequest();
        }


        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<UserAddRequest>> AddUser(UserAddRequest user)
        {
            if (user == null
                || string.IsNullOrEmpty(user.Name)
                || string.IsNullOrEmpty(user.Password)
                || string.IsNullOrEmpty(user.Email)
                || user.RoleId < 1)
            {
                throw new ArgumentException(Errors.INVALID_USER_MODEL);
            }

            var userModel = _mapper.Map<UserModel>(user);

            if (await _userService.IsDuplicated(userModel))
            {
                throw new Exception(Errors.USER_EXIST);
            }

            var responseData = await _userService.AddNewUser(userModel);

            return Ok(responseData);
        }

        [HttpPost]
        //[Authorize]
        [Route("users-page")]
        public async Task<ActionResult<PageResponse<List<UserModel>>>> GetPage([FromBody] PageRequest pageFilter)
        {
            if (pageFilter == null
                || pageFilter.PageNumber < 0
                || pageFilter.PageSize < 1)
            {
                return BadRequest(Errors.INVALID_USER_MODEL);
            }

            //var userIdClaim = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userIdClaim))
            //{
            //    return Unauthorized();
            //}

            //var userId = long.Parse(userIdClaim);

            var userPage = await _userService.GetPage(pageFilter.PageNumber, pageFilter.PageSize);

            return Ok(userPage);
        }

        #endregion
    }
}
