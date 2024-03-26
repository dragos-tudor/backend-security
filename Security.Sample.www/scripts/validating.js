const ValidationErrors = Object.freeze({
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
const setValidationError = (name, error, errors)=>Object.assign({}, errors, {
        [name]: error
    });
export { ValidationErrors as ValidationErrors };
export { setValidationError as setValidationError };
const toValidatorArray = (validators = [])=>typeof validators === "function" ? [
        validators
    ] : validators;
const isValidProp = (validations)=>(propName)=>!validations[propName];
const isNotEmptyValue = (value)=>!!value;
const isValidObj = (validations)=>Object.getOwnPropertyNames(validations).every(isValidProp(validations));
const validateProps = (obj, validators, errors)=>Object.getOwnPropertyNames(validators).reduce(validateProp(obj, validators, errors), {});
const validateProp = (obj, validators, errors)=>(result, propName)=>Object.assign(result, {
            [propName]: validate(obj[propName], validators[propName], errors)
        });
const validate = (value, validators, errors = ValidationErrors)=>toValidatorArray(validators).map((validator)=>validator(value, errors)).filter(isNotEmptyValue).join("\n");
const validateObj = (obj, validators, errors = ValidationErrors)=>{
    const validations = validateProps(obj, validators, errors);
    const isValid = isValidObj(validations);
    return Object.assign(validations, {
        isValid
    });
};
export { validate as validate, validateObj as validateObj };
const isEmptyString = (value)=>value === "";
const isEmptyValue = (value)=>isNullValue(value) || isEmptyString(value);
const isNullValue = (value)=>value == null;
const contains = (token)=>(str, errors = ValidationErrors)=>isEmptyValue(str) || str.includes(token) ? null : errors["contains"].replace("#token", token);
const isDateValue = (value)=>{
    if (value instanceof Date) return true;
    if (typeof value !== "string") return false;
    return value.match(/\d{4}-\d{2}-\d{2}/g) && !isNaN(Date.parse(value));
};
const isDate = (val, errors = ValidationErrors)=>isEmptyValue(val) || isDateValue(val) ? null : errors["isDate"];
const isEmail = (str, errors = ValidationErrors)=>isEmptyValue(str) || str.match(/[a-zA-Z]+\w*@[a-zA-Z]+(\w|\-)*\.[a-zA-Z]+\w+/g) ? null : errors["isEmail"];
const isInteger = (val, errors = ValidationErrors)=>isEmptyValue(val) || !isNaN(val) && val % 1 === 0 ? null : errors["isInteger"];
const isNumber = (val, errors = ValidationErrors)=>isEmptyValue(val) || !isNaN(val) ? null : errors["isNumber"];
const isString = (val, errors = ValidationErrors)=>isEmptyValue(val) || typeof val === "string" ? null : errors["isString"];
const urlRegExp = /^(http(s)?:\/\/)([\da-z\.-]+\.[a-z\.]{2,6}|[\d\.]+)([\/:?=&#]{1}[\da-z\.-]+)*[\/\?]?$/i;
const isUrl = (val, errors = ValidationErrors)=>isEmptyValue(val) || urlRegExp.test(val) ? null : errors["isUrl"];
const isRequired = (val, errors = ValidationErrors)=>isEmptyValue(val) ? errors["isRequired"] : null;
const replaceMaxPlaceholder = (error, max)=>(error || "").replace("#max", max);
const hasMaxLength = (max)=>(str, errors = ValidationErrors)=>isEmptyValue(str) || str.length <= max ? null : replaceMaxPlaceholder(errors["hasMaxLength"], max);
const replaceMinPlaceholder = (error, min)=>(error || "").replace("#min", min);
const hasMinLength = (min)=>(str, errors = ValidationErrors)=>isEmptyValue(str) || str.length >= min ? null : replaceMinPlaceholder(errors["hasMinLength"], min);
const replaceMinMaxPlaceholder = (error, min, max)=>(error || "").replace("#min", min).replace("#max", max);
const inRange = (min, max)=>(val, errors = ValidationErrors)=>isEmptyValue(val) || val >= min && val <= max ? null : replaceMinMaxPlaceholder(errors["inRange"], min, max);
const matchRegExp = (pattern)=>(str, errors = ValidationErrors)=>isEmptyValue(str) || pattern.test(str) ? null : errors["matchRegExp"];
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
