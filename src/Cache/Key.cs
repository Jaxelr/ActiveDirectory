using System;
using System.Collections.Generic;
using System.Linq;
using ActiveDirectory.Extensions;

namespace ActiveDirectory
{
    internal static class Key
    {
        public static string FieldSeparator = ":";

        /// <summary>
        /// Create a string key based on the typeof<T> and the field sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <returns>A string with the type and all fields concatenated delimited by the field separator</returns>
        public static string Create<T>(string field) => Create(typeof(T), field);

        /// <summary>
        /// Create a string key based on the typeof<T> and the fields sent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields"></param>
        /// <returns>A string with the type and all fields concatenated delimited by the field separator</returns>
        public static string Create<T>(params string[] fields) => Create(typeof(T), fields.Select(x => x));

        /// <summary>
        /// Create  string key based on the type and the fields sent
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fields"></param>
        /// <returns>A string with the type and all fields concatenated delimited by the field separator</returns>
        public static string Create(Type type, params string[] fields) => Create(type, fields.Select(x => x));

        /// <summary>
        /// Create a string key based on the type and the fields sent
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fields"></param>
        /// <returns>A string with the type and all fields concatenated delimited by the field separator</returns>
        public static string Create(Type type, IEnumerable<string> fields)
        {
            if (type == null)
            {
                throw new ArgumentNullException($"Argument {nameof(type)} cannot be null");
            }

            if (fields == null)
            {
                throw new ArgumentNullException($"Argument {nameof(fields)} cannot be null");
            }

            string urn;

            if (type.IsIEnumerable() || type.IsArray)
            {
                urn = $"{type.Name}{FieldSeparator}{type.GetAnyElementType().Name}";
            }
            else
            {
                urn = type.Name;
            }

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
        /// <returns>A string with the type and field concatenated delimited by the field separator</returns>
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

            if (type.IsIEnumerable() || type.IsArray)
            {
                return $"{type.Name}{FieldSeparator}{type.GetAnyElementType().Name}{FieldSeparator}{field}";
            }

            return $"{type.Name}{FieldSeparator}{field}";
        }
    }
}
