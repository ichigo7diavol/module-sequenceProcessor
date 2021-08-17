using System;
using System.Linq;
using System.Reflection;
using Services.SequenceProcessor.Steps;

namespace Services.SequenceProcessor.Utils
{
    public static class TypeExtensions
    {
        public static bool TryGetInterfaceByGenericDefinition(this Type current, 
            Type definition, out Type type)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            if (definition == null)
            {
                throw new ArgumentNullException(nameof(definition));
            }
            if (!definition.IsInterface)
            {
                throw new InvalidOperationException($"{definition} Is Not Interface!");
            }
            if (!definition.IsGenericTypeDefinition)
            {
                throw new InvalidOperationException($"{definition} Is Not GenericTypeDefinition!");
            }
            type = current.GetInterfaces().
                FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == definition);

            return type != null;
        }

        public static bool TryGetParametrizedStepDefinition(this ISequenceStep step, out Type type)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            return TryGetInterfaceByGenericDefinition(step.GetType(), 
                Constants.ParameterizedStepInterface, out type);
        }
        
        public static bool TryGetContextualStepDefinition(this ISequenceStep step, out Type type)
        {
            if (step == null)
            {
                throw new ArgumentNullException(nameof(step));
            }
            return TryGetInterfaceByGenericDefinition(step.GetType(), 
                Constants.ContextualStepInterface, out type);
        }

        public static Type GetContextualStepDtoType(this Type current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            if (!current.IsGenericType || Constants.ContextualStepInterface != current.GetGenericTypeDefinition())
            {
                throw new InvalidOperationException(
                    $"Expected Specification Of Type {Constants.ContextualStepInterface}");
            }
            return current.GenericTypeArguments[Constants.ContextualStepDtoArgumentIndex];
        }
        
        public static Type GetParametrizedStepDtoType(this Type current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            if (!current.IsGenericType || Constants.ParameterizedStepInterface != current.GetGenericTypeDefinition())
            {
                throw new InvalidOperationException(
                    $"Expected Specification Of Type {Constants.ParameterizedStepInterface}");
            }
            return current.GenericTypeArguments[Constants.ParameterizedStepDtoArgumentIndex];
        }
        
        public static Type GetContextualStepContextType(this Type current)
        {
            if (current == null)
            {
                throw new ArgumentNullException(nameof(current));
            }
            if (!current.IsGenericType || Constants.ContextualStepInterface != current.GetGenericTypeDefinition())
            {
                throw new InvalidOperationException(
                    $"Expected Specification Of Type {Constants.ContextualStepInterface}");
            }
            return current.GenericTypeArguments[Constants.ContextualStepContextArgumentIndex];
        }

        // TODO: To Factory        
        public static ConstructorInfo GetParametrizedStepHandlerCtorInfo(this Type typeDefinition, Type dto)
        {
            if (typeDefinition == null)
            {
                throw new ArgumentNullException(nameof(typeDefinition));
            }
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var arguments = new Type[Constants.ParametrizedCtorArgumentsCount];

            arguments[Constants.ParameterizedStepDtoArgumentIndex] = dto;

            var specification = Constants.ParameterizedStepHandler.
                MakeGenericType(arguments);
            
            var ctor = specification.GetConstructor(new[] {typeDefinition});

            return ctor;
        }
        
        // TODO: To Factory      
        public static ConstructorInfo GetContextualStepHandlerCtorInfo(this Type typeDefinition, Type context, Type dto)
        {
            if (typeDefinition == null)
            {
                throw new ArgumentNullException(nameof(typeDefinition));
            }
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var arguments = new Type[Constants.ContextualCtorArgumentsCount];

            arguments[Constants.ContextualStepDtoArgumentIndex] = dto;
            arguments[Constants.ContextualStepContextArgumentIndex] = context;

            var specification = Constants.ContextualStepHandler.
                MakeGenericType(arguments);
            
            var ctor = specification.GetConstructor(new[] {typeDefinition, Constants.ContextProvider});

            return ctor;
        }
    }
}