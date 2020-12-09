using System;

namespace Contracts.PublicApi.DTO
{
    public interface IPublicDTOMapper<TLeft, TRight>
    {
        public TLeft Map(TRight inObject);
        public TRight Map(TLeft outObject);

    }
}