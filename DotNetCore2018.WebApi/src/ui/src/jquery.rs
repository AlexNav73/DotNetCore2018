use js_sys::Promise;
use wasm_bindgen::prelude::*;

#[wasm_bindgen]
extern "C" {
    pub type Element;

    #[wasm_bindgen(structural, method)]
    pub fn val(this: &Element) -> String;

    #[wasm_bindgen(structural, method, js_name = "val")]
    pub fn set_val(this: &Element, val: &str) -> Promise;

    #[wasm_bindgen(structural, method, js_name = "data")]
    pub fn data_by_key(this: &Element, attr: &str) -> JsValue;
}

#[wasm_bindgen]
pub fn ui_parse_val(elem: &Element) -> Promise {
    console_error_panic_hook::set_once();

    console_log!("{}", elem.val());
    let p = elem.set_val(&"");
    console_log!("{}", elem.val());
    p
}

#[wasm_bindgen]
pub fn ui_find_elem_by_id(elem: &Element, data_attr: &str) -> Result<Promise, JsValue> {
    console_error_panic_hook::set_once();

    if let Some(category_id) = elem.data_by_key(data_attr).as_f64() {
        Ok(crate::ui_category_delete(category_id as u32))
    } else {
        Err("data attribute not found".into())
    }
}