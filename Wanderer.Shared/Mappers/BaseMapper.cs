namespace Wanderer.Shared.Mappers;

public abstract class BaseMapper<TEntity, TDto, TInsertDto> : IBaseMapper<TEntity, TDto, TInsertDto> where TEntity : class
                                                                                                  where TDto : class
                                                                                                  where TInsertDto : class
{
    public abstract TDto MapToDto(TEntity entity);

    public abstract TEntity MapToEntity(TDto dto);

    public abstract TEntity MapToEntity(TInsertDto insertDto, params object[] parameters);
}
