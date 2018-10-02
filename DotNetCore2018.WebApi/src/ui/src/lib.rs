use js_sys::Promise;
use wasm_bindgen::prelude::*;
use wasm_bindgen_futures::future_to_promise;
use wasm_bindgen_futures::JsFuture;
use web_sys::{Request, RequestInit};

#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;

macro_rules! console_log {
    ($($t:tt)*) => (web_sys::console::log_1(&format_args!($($t)*).to_string().into()))
}

#[wasm_bindgen]
extern "C" {
    pub type Element;

    #[wasm_bindgen(structural, method)]
    pub fn val(this: &Element) -> String;

    #[wasm_bindgen(structural, method, js_name = "val")]
    pub fn set_val(this: &Element, val: &str) -> Promise;
}

#[wasm_bindgen]
pub fn ui_parse_val(elem: &Element) -> Promise {
    console_log!("{}", elem.val());
    let p = elem.set_val(&"");
    console_log!("{}", elem.val());
    p
}

#[wasm_bindgen]
pub fn ui_find_elem_by_id(elem_id: &str, data_attr: &str) -> Result<Promise, JsValue> {
    console_error_panic_hook::set_once();

    let document = web_sys::window().unwrap().document().unwrap();
    let element = document.get_element_by_id(elem_id).ok_or("can't find element by id")?;
    let category_id = element.get_attribute(data_attr).unwrap();
    console_log!("category id is {}", category_id);
    Ok(ui_category_delete(category_id.parse().unwrap()))
}

#[wasm_bindgen]
pub fn ui_category_delete(id: u32) -> Promise {
    console_error_panic_hook::set_once();

    let url = format!("../api/v1/categoryapi/delete?id={}", id);

    let mut init = RequestInit::new();
    init.method("DELETE");
    let request = Request::new_with_str_and_init(&url, &init).unwrap();
    let window = web_sys::window().unwrap();
    let request_promise = window.fetch_with_request(&request);

    future_to_promise(JsFuture::from(request_promise))
}