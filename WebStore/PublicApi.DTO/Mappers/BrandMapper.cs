using System;

namespace PublicApi.DTO.Mappers
{
    public class BrandMapper : IPublicDTOMapper<BLL.App.DTO.Brand, v2.Brand>
    {
        // public api dto to bll dto
        public BLL.App.DTO.Brand Map(v2.Brand inObject)
        {
            return new BLL.App.DTO.Brand()
            {
                Id = inObject.Id,
                Description = inObject.Description,
                UserId = inObject.UserId.GetValueOrDefault(),
                Name = inObject.Name
            };
        }

        //  bll dto to public api dto 
        public v2.Brand Map(BLL.App.DTO.Brand outObject)
        {
            return new v2.Brand()
            {
                Id = outObject.Id,
                Description = outObject.Description,
                UserId = outObject.UserId,
                Name = outObject.Name
            };
        }
        
        // from createDto to bll dto
        public BLL.App.DTO.Brand BrandCreate(v2.BrandCreate brandCreateDTO)
        {
            var brand = new BLL.App.DTO.Brand()
            {
                Id = Guid.NewGuid(),
                UserId = brandCreateDTO.UserId!.Value,
                Name = brandCreateDTO.Name,
                Description = brandCreateDTO.Description,
            };
            return brand;
        }
    }
}