namespace BlueSkynet.Domain.Localization
{
    internal static class ExceptionMessages
    {
        /// <summary>Failed to parse configuration section {0} due to {1}.</summary>
        public const string FailedToParseConfigurationSection_SectionName_ErrorMsg = "Failed to parse configuration section {0} due to {1}.";

        /// <summary>Collection parameter {0} can't be empty.</summary>
        public const string ParamCollectionCannotBeEmpty_ParamName = "Collection parameter {0} can't be empty.";

        /// <summary>Array '{0}' length can't be less than {1}.</summary>
        public const string ParamArrayLengthIsToShort_ParamName_MinimumAllowedLength = "Array '{0}' length can't be less than {1}.";

        /// <summary>Value '{1}' supplied to parameter '{0} is outside valid range of [{2}-{3}].</summary>
        public const string ParamOutOfRange_ParamName_Value_MinRangeValue_MaxRangeValue = "Value '{1}' supplied to parameter '{0} is outside valid range of [{2}-{3}].";

        /// <summary>Sequence can't be equal for parameters {0} and {1}.</summary>
        public const string SequenceCannotBeEqualForParams_ParamA_ParamB = "Sequence can't be equal for parameters {0} and {1}.";

        /// <summary>String '{0}' parameter can't be blank.</summary>
        public const string StringParamCannotBeBlank_ParamName = "String '{0}' parameter can't be blank.";

        /// <summary>Uri '{0}' is not an absolute address.</summary>
        public const string UriParamMustBeAbsolute_UriAddress = "Uri '{0}' is not an absolute address.";

        /// <summary>Uri '{0}' must have one of the following schemas: {1}.</summary>
        public const string UriParamSchemaNotAllowed_UriAddress_AllowedSchemas = "Uri '{0}' must have one of the following schemas: {1}.";

        /// <summary>Message {0} is not in a valid state. It contained the following validation errors: {1}</summary>
        public const string MessageIsNotInValidState_MessageType_ValidationErrors = "Message {0} is not in a valid state. It contained the following validation errors: {1}";
    }
}