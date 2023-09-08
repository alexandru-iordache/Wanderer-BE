namespace Wanderer.Infrastructure.Mappers.Generics;

public abstract class GenericMapper<TEntity, TDto, TInsertDto> : IGenericMapper<TEntity, TDto, TInsertDto> where TEntity : class
                                                                                                  where TDto : class
                                                                                                  where TInsertDto : class
{
    public virtual TDto MapToDto(TEntity entity)
    {
        return null;
    }

    public virtual TEntity MapToEntity(TDto dto)
    {
        return null;
    }

    public virtual TEntity MapToEntity(TInsertDto insertDto)
    {
        return null;
    }
}
