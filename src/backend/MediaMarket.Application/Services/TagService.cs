using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Contracts.Repositories;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Tag;
using MediaMarket.Application.DTO.Response.Tag;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Services
{
    public class TagService(ITagRepository tagRepository, IMapper mapper) : BaseResponseHandler, ITagService
    {
        private readonly ITagRepository _tagRepository = tagRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<IEnumerable<TagResponse>>> GetAll()
        {
            var tags = await _tagRepository.GetAllAsync();
            return Success(_mapper.Map<IEnumerable<TagResponse>>(tags));
        }

        public async Task<BaseResponse<TagResponse>> Create(CreateTagRequest request)
        {
            var tag = await _tagRepository.AddAsync(_mapper.Map<Tag>(request));
            await _tagRepository.SaveChangesAsync();
            return Created(_mapper.Map<TagResponse>(tag));
        }

        public async Task<BaseResponse<TagResponse>> Update(Guid id, UpdateTagRequest request)
        {
            var tag = await _tagRepository.FindByIdAsync(id);
            if (tag == null)
            {
                return NotFound<TagResponse>("Tag not found!");
            }

            _mapper.Map(request, tag);
            await _tagRepository.SaveChangesAsync();

            return Success(_mapper.Map<TagResponse>(tag));
        }

        public async Task<BaseResponse<Guid>> Delete(Guid id)
        {
            var tag = await _tagRepository.FindByIdAsync(id);
            if (tag == null)
            {
                return NotFound<Guid>("Tag not found!");
            }

            _tagRepository.Remove(tag);
            await _tagRepository.SaveChangesAsync();

            return Deleted<Guid>();
        }
    }
}
