using System;

namespace Strix.Helpers
{
    /// <summary>
    /// Helper class to simplify work with exception throwing, especially with <see cref="ArgumentNullException"/> and <see cref="ArgumentException"/>.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Wraps <see cref="ArgumentNullException"/> throwing.
        /// Checks if argument equals to <see langword="null"/>.
        /// If <see langword="true"/>, throws an <see cref="ArgumentNullException"/> with information about an argument.
        /// Otherwise, continues code execution.
        /// </summary> 
        /// <param name="sourceArgument">Argument to check.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <exception cref="ArgumentNullException">If <paramref name="sourceArgument"/> equals to <see langword="null"/>.</exception>
        public static void ThrowIfArgumentEqualsToNull<T>(this T sourceArgument, string argumentName)
        {
            if (sourceArgument == null)
            {
                string exceptionMessage = $"Argument '{argumentName}' ({typeof(T).Name}) equals to null.";
                
                NullReferenceException innerNullReferenceException = new NullReferenceException(exceptionMessage);

                ArgumentNullException argumentNullException = new ArgumentNullException(exceptionMessage, innerNullReferenceException);

                throw argumentNullException;
            }
        }

        /// <summary>
        /// Wraps <see cref="ArgumentNullException"/> throwing.
        /// Checks if argument equals to <see langword="null"/>.
        /// If <see langword="true"/>, throws an <see cref="ArgumentNullException"/> with information about an argument.
        /// Otherwise, continues code execution and returns <paramref name="sourceArgument"/> value. 
        /// </summary>
        /// <param name="sourceArgument">Argument to check.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <exception cref="ArgumentNullException">If <paramref name="sourceArgument"/> equals to <see langword="null"/>.</exception>
        /// <returns><paramref name="sourceArgument"/> value.</returns>
        public static T GetOrThrowIfArgumentEqualsToNull<T>(this T sourceArgument, string argumentName)
        {
            sourceArgument.ThrowIfArgumentEqualsToNull(argumentName);

            return sourceArgument;
        }

        /// <summary>
        /// Wraps <see cref="ArgumentNullException"/> throwing.
        /// Firstly, checks <paramref name="sourceArgument"/> for equality to null.
        /// Secondly, matches the value to satisfy the <paramref name="argumentMatchFunc"/> matcher. 
        /// </summary>
        /// <param name="sourceArgument">Argument to check.</param>
        /// <param name="argumentName">Argument name.</param>
        /// <param name="argumentMatchFunc">Func to match <paramref name="sourceArgument"/> value for specified rules.</param>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <exception cref="ArgumentNullException">If <paramref name="sourceArgument"/> equals to <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="sourceArgument"/> does not match specified rules, described in <paramref name="argumentMatchFunc"/>.</exception>
        public static void ThrowIfArgumentEqualsToNullOrDoesNotMatch<T>(this T sourceArgument, string argumentName, Func<T, bool> argumentMatchFunc)
        {
            sourceArgument.ThrowIfArgumentEqualsToNull(argumentName);

            argumentMatchFunc.ThrowIfArgumentEqualsToNull(nameof(argumentMatchFunc));
            
            bool matchResult = argumentMatchFunc.Invoke(sourceArgument);

            if (!matchResult)
            {
                string exceptionMessage = $"Argument '{argumentName}' ({typeof(T).Name}) does not comply with the specified rules.";
                
                throw new ArgumentException(exceptionMessage);
            }
        }

        public static void ThrowIfPropertyEqualsToNull<T>(this T sourceProperty, string propertyName)
        {
            if (sourceProperty == null)
            {
                string exceptionMessage = $"Property '{propertyName}' ({typeof(T).Name}) equals to null.";
            
                NullReferenceException innerNullReferenceException = new NullReferenceException(exceptionMessage);
                
                InvalidOperationException invalidOperationException =
                    new InvalidOperationException(exceptionMessage, innerNullReferenceException);
                
                throw invalidOperationException;
            }
        }
    }
}