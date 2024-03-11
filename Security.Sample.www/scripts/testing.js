// deno-fmt-ignore-file
// deno-lint-ignore-file
// This code was bundled using `deno bundle` and it's not recommended to edit it manually

const getElementTextContent = (elem)=>elem.textContent;
const findHtmlAscendant = (elem, func)=>{
    while(elem){
        if (func(elem)) return elem;
        elem = elem.parentElement;
    }
};
const findHtmlDescendant = (elem, func)=>{
    const elems = [
        elem
    ];
    for (const elem1 of elems){
        if (func(elem1)) return elem1;
        elems.push(...Array.from(elem1.children));
    }
};
const isDisabledFieldset = (elem)=>elem.tagName === "FIELDSET" && elem.disabled;
const isDisabledElement = (elem)=>elem.disabled || findHtmlAscendant(elem, isDisabledFieldset) !== undefined;
const validateElement = (elem, props)=>{
    if (!elem) throw new Error(`Element not found with props '${JSON.stringify(props)}'.`);
    return elem;
};
const isPropEquality = (props, elem)=>(propName)=>propName === "className" ? elem[propName]?.includes(props[propName]) : elem[propName] === props[propName];
const isPropsEquality = (props)=>(elem)=>Object.keys(props).every(isPropEquality(props, elem));
const getByProps = (elem, props)=>validateElement(findHtmlDescendant(elem, isPropsEquality(props)), props);
const getByClass = (elem, value, props)=>getByProps(elem, {
        ...props,
        className: value
    });
const getByPlaceholder = (elem, value, props)=>getByProps(elem, {
        ...props,
        placeholder: value
    });
const getByRole = (elem, value, props)=>getByProps(elem, {
        ...props,
        role: value
    });
const getByTag = (elem, value, props)=>getByProps(elem, {
        ...props,
        tagName: value?.toUpperCase()
    });
const getByType = (elem, value, props)=>getByProps(elem, {
        ...props,
        type: value
    });
export { getByClass as getByClass, getByProps as getByProps, getByPlaceholder as getByPlaceholder, getByRole as getByRole, getByTag as getByTag, getByType as getByType };
const screen = {
    text: getElementTextContent,
    disabled: isDisabledElement
};
export { screen as screen };
const getWaitingStatus = (func, totalTime, timeout)=>{
    if (totalTime > timeout) return "timeout";
    if (func()) return "completed";
    return "continue";
};
const waitFor = (func, timeout = 500, delay = 10)=>{
    let intervals = 0;
    return new Promise((resolve, reject)=>{
        const clearId = setInterval(()=>{
            const totalTime = delay * intervals++;
            switch(getWaitingStatus(func, totalTime, timeout)){
                case "timeout":
                    clearInterval(clearId);
                    return reject(Error("Timeout reached."));
                case "completed":
                    clearInterval(clearId);
                    return resolve(true);
            }
        }, delay);
    });
};
const waitForAsyncs = (func)=>new Promise(function(resolve) {
        const timeoutId = setTimeout(()=>{
            clearTimeout(timeoutId);
            resolve(func?.());
        }, 0);
    });
const getEventOptions = (detail)=>({
        bubbles: true,
        cancelable: true,
        detail
    });
const createEvent = (eventName, data)=>new CustomEvent(eventName, getEventOptions(data));
const fireEvent = (elem, eventName, data)=>{
    elem.addEventListener(eventName, ()=>{});
    return elem.dispatchEvent(createEvent(eventName, data));
};
const clickElement = (elem)=>elem.type === "submit" ? fireEvent(elem, "submit") : fireEvent(elem, "click");
const clickElementAndWait = async (elem)=>{
    clickElement(elem);
    return await waitForAsyncs();
};
const validateInputElement = (elem)=>{
    if (!elem) throw new Error(`Element '${elem.tagName}' should be input.`);
    return elem;
};
const writeElementValue = (elem, value)=>{
    validateInputElement(elem);
    elem.value = value;
    return fireEvent(elem, "change");
};
const user = {
    click: clickElement,
    clickAndWait: clickElementAndWait,
    fireEvent,
    waitFor,
    waitForAsyncs,
    write: writeElementValue
};
export { waitFor };
export { waitForAsyncs };
export { user as user };
