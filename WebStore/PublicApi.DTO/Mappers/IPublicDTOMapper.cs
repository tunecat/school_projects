namespace PublicApi.DTO.Mappers
{
    public interface IPublicDTOMapper<TLeft, TRight>
    {
        public TLeft Map(TRight inObject);
        public TRight Map(TLeft outObject);

    }
}