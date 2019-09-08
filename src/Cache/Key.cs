using System;

namespace ActiveDirectory
{
    public class Key
    {
        public static string FieldSeparator = "::";

        /// <summary>
        /// Create a string key based on the typeof<T> and the field sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string Create<T>(string field) => Create(typeof(T), field);

        /// <summary>
        /// Create a string key based on the typeof<T> and the fields sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string Create<T>(params string[] fields) => Create(typeof(T), fields);

        /// <summary>
        /// Create a string key based on the type and the fields sent
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static string Create(Type type, params string[] fields)
        {
            if (type == null)
            {
                throw new ArgumentNullException($"Argument {nameof(type)} cannot be null");
            }

            if (fields == null)
            {
                throw new ArgumentNullException($"Argument {nameof(fields)} cannot be null");
            }

            string urn = type.Name;

            foreach (string field in fields)
            {
                urn += FieldSeparator;
                urn += field;
            }

            return urn;
        }

        /// <summary>
        /// Create a string key based on the type and the field sent
        /// </summary>
        /// <param name="type"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string Create(Type type, string field)
        {
            if (type == null)
            {
                throw new ArgumentNullException($"Argument {nameof(type)} cannot be null");
            }

            if (field == null)
            {
                throw new ArgumentNullException($"Argument {nameof(field)} cannot be null");
            }

            return $"{type.Name}{FieldSeparator}{field}";
        }
    }
}
