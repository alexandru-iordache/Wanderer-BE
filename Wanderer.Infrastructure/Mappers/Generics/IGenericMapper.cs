namespace Wanderer.Infrastructure.Mappers.Generics
{
    public interface IGenericMapper<TEntity, TDto, TInsertDto>
        where TEntity : class
        where TDto : class
        where TInsertDto : class
    {
        TDto MapToDto(TEntity entity);

        TEntity MapToEntity(TDto dto);

        TEntity MapToEntity(TInsertDto insertDto);
    }
}