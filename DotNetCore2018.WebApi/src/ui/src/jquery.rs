use js_sys::Promise;
use wasm_bindgen::prelude::*;

#[wasm_bindgen]
extern "C" {
    pub type Element;

    #[wasm_bindgen(structural, method)]
    pub fn val(this: &Element) -> String;

    #[wasm_bindgen(structural, method, js_name = "val")]
    pub fn set_val(this: &Element, val: &str) -> Promise;

    #[wasm_bindgen(structural, method, js_name = "text")]
    pub fn set_text(this: &Element, val: &str) -> Promise;

    #[wasm_bindgen(structural, method, js_name = "data")]
    pub fn data_by_key(this: &Element, attr: &str) -> JsValue;
}