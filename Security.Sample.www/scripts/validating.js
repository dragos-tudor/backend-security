const getEnglishErrors = ()=>Object.freeze({
        contains: 'value not contains "#token"',
        hasMaxLength: "maximum #max characters allowed",
        hasMinLength: "minimum #min characters required",
        isDate: "invalid date",
        isEmail: "invalid email",
        isInteger: "invalid integer",
        isNumber: "invalid number",
        inRange: "value is not between #min and #max",
        isRequired: "value is required",
        isString: "invalid string",
        isUrl: "invalid url",
        matchRegExp: "value not match expression"
    });
const extendValidationErrors = (name, message, errors)=>Object.assign({}, errors, {
        [name]: message
    });
let validationErrors = getEnglishErrors();
const getValidationErrors = ()=>validationErrors;
const setValidationErrors = (errors)=>validationErrors = errors;
const setValidationError = (validatorName, error, errors = validationErrors)=>validationErrors = extendValidationErrors(validatorName, error, errors);
export { getValidationErrors as getValidationErrors, setValidationError as setValidationError, setValidationErrors as setValidationErrors, validationErrors as validationErrors };
const toValidatorArray = (validators = [])=>typeof validators === "function" ? [
        validators
    ] : validators;
const isValidProp = (validations)=>(propName)=>!validations[propName];
const isNotEmptyValue = (value)=>!!value;
const isValidObj = (validations)=>Object.getOwnPropertyNames(validations).every(isValidProp(validations));
const validateObj = (obj, validators)=>Object.getOwnPropertyNames(validators).reduce(validateProp(obj, validators), {});
const validateProp = (obj, validators)=>(result, propName)=>Object.assign(result, {
            [propName]: validate(obj[propName], validators[propName])
        });
const validate = (value, validators, errors = validationErrors)=>toValidatorArray(validators).map((validator)=>validator(value, errors)).filter(isNotEmptyValue).join("\n");
const validateProps = (obj, validators, errors = validationErrors)=>{
    const validations = validateObj(obj, validators, errors);
    const isValid = isValidObj(validations);
    return Object.assign(validations, {
        isValid
    });
};
export { validate as validate, validateProps as validateProps };
const isEmptyString = (value)=>value === "";
const isEmptyValue = (value)=>isNullValue(value) || isEmptyString(value);
const isNullValue = (value)=>value == null;
const contains = (token)=>(str, errors = validationErrors)=>isEmptyValue(str) || str.includes(token) ? null : errors["contains"].replace("#token", token);
const isDateValue = (value)=>{
    if (value instanceof Date) return true;
    if (typeof value !== "string") return false;
    return value.match(/\d{4}-\d{2}-\d{2}/g) && !isNaN(Date.parse(value));
};
const isDate = (val, errors = validationErrors)=>isEmptyValue(val) || isDateValue(val) ? null : errors["isDate"];
const isEmail = (str, errors = validationErrors)=>isEmptyValue(str) || str.match(/[a-zA-Z]+\w*@[a-zA-Z]+(\w|\-)*\.[a-zA-Z]+\w+/g) ? null : errors["isEmail"];
const isInteger = (val, errors = validationErrors)=>isEmptyValue(val) || !isNaN(val) && val % 1 === 0 ? null : errors["isInteger"];
const isNumber = (val, errors = validationErrors)=>isEmptyValue(val) || !isNaN(val) ? null : errors["isNumber"];
const isString = (val, errors = validationErrors)=>isEmptyValue(val) || typeof val === "string" ? null : errors["isString"];
const urlRegExp = /^(http(s)?:\/\/)([\da-z\.-]+\.[a-z\.]{2,6}|[\d\.]+)([\/:?=&#]{1}[\da-z\.-]+)*[\/\?]?$/i;
const isUrl = (val, errors = validationErrors)=>isEmptyValue(val) || urlRegExp.test(val) ? null : errors["isUrl"];
const isRequired = (val, errors = validationErrors)=>isEmptyValue(val) ? errors["isRequired"] : null;
const replaceMaxPlaceholder = (error, max)=>(error || "").replace("#max", max);
const hasMaxLength = (max)=>(str, errors = validationErrors)=>isEmptyValue(str) || str.length <= max ? null : replaceMaxPlaceholder(errors["hasMaxLength"], max);
const replaceMinPlaceholder = (error, min)=>(error || "").replace("#min", min);
const hasMinLength = (min)=>(str, errors = validationErrors)=>isEmptyValue(str) || str.length >= min ? null : replaceMinPlaceholder(errors["hasMinLength"], min);
const replaceMinMaxPlaceholder = (error, min, max)=>(error || "").replace("#min", min).replace("#max", max);
const inRange = (min, max)=>(val, errors = validationErrors)=>isEmptyValue(val) || val >= min && val <= max ? null : replaceMinMaxPlaceholder(errors["inRange"], min, max);
const matchRegExp = (pattern)=>(str, errors = validationErrors)=>isEmptyValue(str) || pattern.test(str) ? null : errors["matchRegExp"];
export { contains as contains };
export { isDate as isDate };
export { isEmail as isEmail };
export { isInteger as isInteger };
export { isNumber as isNumber };
export { isString as isString };
export { isUrl as isUrl };
export { isRequired as isRequired };
export { hasMaxLength as hasMaxLength };
export { hasMinLength as hasMinLength };
export { inRange as inRange };
export { matchRegExp as matchRegExp };
