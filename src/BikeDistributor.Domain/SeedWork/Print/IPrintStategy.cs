namespace BikeDistributor.Domain.SeedWork.Print
{
    /// <summary>
    /// Prints <see cref="TDto"/> according to specifyied strategy.
    /// </summary>
    /// <typeparam name="TDto">Dto to be printed</typeparam>
    public interface IPrintStategy<TDto>
    {
        /// <summary>
        /// Prints <see cref="TDto"/> according to specifyied strategy.
        /// </summary>
        /// <param name="tDto">Dto to be printed</param>
        /// <returns>Printed <see cref="TDto"/></returns>
        string Print(TDto tDto);
    }
}
