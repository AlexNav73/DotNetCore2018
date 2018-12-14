(window["webpackJsonp"] = window["webpackJsonp"] || []).push([[0],{

/***/ "./src/ui.js":
/*!*******************!*\
  !*** ./src/ui.js ***!
  \*******************/
/*! exports provided: __wbg_error_cc95a3d302735ca3, __widl_f_new_with_str_and_init_Request, __widl_instanceof_Response, __widl_f_json_Response, __widl_instanceof_Window, __widl_f_fetch_with_request_Window, __wbg_newnoargs_b1f726fad978f5a3, __wbg_call_fa7f0da29d7b9250, __wbg_call_bd08bd79389c3e82, __wbg_new_70e5fc804058d6a0, __wbg_set_3387446d3253486c, __wbg_stringify_cc97edb3bc4849c0, __wbg_new_477a6c8ec5821b36, __wbg_then_b902ad50736efa21, __wbg_text_6ad386c63bfa0000, ui_category_delete, ui_product_delete, ui_supplier_delete, ui_show_list, __wbindgen_object_clone_ref, __wbindgen_object_drop_ref, __wbindgen_string_new, __wbindgen_number_get, __wbindgen_is_null, __wbindgen_is_undefined, __wbindgen_boolean_get, __wbindgen_is_symbol, __wbindgen_string_get, __wbindgen_cb_drop, __wbindgen_closure_wrapper137, __wbindgen_throw */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_error_cc95a3d302735ca3", function() { return __wbg_error_cc95a3d302735ca3; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__widl_f_new_with_str_and_init_Request", function() { return __widl_f_new_with_str_and_init_Request; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__widl_instanceof_Response", function() { return __widl_instanceof_Response; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__widl_f_json_Response", function() { return __widl_f_json_Response; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__widl_instanceof_Window", function() { return __widl_instanceof_Window; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__widl_f_fetch_with_request_Window", function() { return __widl_f_fetch_with_request_Window; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_newnoargs_b1f726fad978f5a3", function() { return __wbg_newnoargs_b1f726fad978f5a3; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_call_fa7f0da29d7b9250", function() { return __wbg_call_fa7f0da29d7b9250; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_call_bd08bd79389c3e82", function() { return __wbg_call_bd08bd79389c3e82; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_new_70e5fc804058d6a0", function() { return __wbg_new_70e5fc804058d6a0; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_set_3387446d3253486c", function() { return __wbg_set_3387446d3253486c; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_stringify_cc97edb3bc4849c0", function() { return __wbg_stringify_cc97edb3bc4849c0; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_new_477a6c8ec5821b36", function() { return __wbg_new_477a6c8ec5821b36; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_then_b902ad50736efa21", function() { return __wbg_then_b902ad50736efa21; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbg_text_6ad386c63bfa0000", function() { return __wbg_text_6ad386c63bfa0000; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ui_category_delete", function() { return ui_category_delete; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ui_product_delete", function() { return ui_product_delete; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ui_supplier_delete", function() { return ui_supplier_delete; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ui_show_list", function() { return ui_show_list; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_object_clone_ref", function() { return __wbindgen_object_clone_ref; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_object_drop_ref", function() { return __wbindgen_object_drop_ref; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_string_new", function() { return __wbindgen_string_new; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_number_get", function() { return __wbindgen_number_get; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_is_null", function() { return __wbindgen_is_null; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_is_undefined", function() { return __wbindgen_is_undefined; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_boolean_get", function() { return __wbindgen_boolean_get; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_is_symbol", function() { return __wbindgen_is_symbol; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_string_get", function() { return __wbindgen_string_get; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_cb_drop", function() { return __wbindgen_cb_drop; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_closure_wrapper137", function() { return __wbindgen_closure_wrapper137; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "__wbindgen_throw", function() { return __wbindgen_throw; });
/* harmony import */ var _ui_bg__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./ui_bg */ "./src/ui_bg.wasm");
/* tslint:disable */


const __wbg_error_cc95a3d302735ca3_target = console.error;

const TextDecoder = typeof self === 'object' && self.TextDecoder
    ? self.TextDecoder
    : __webpack_require__(/*! util */ "./node_modules/util/util.js").TextDecoder;

let cachedDecoder = new TextDecoder('utf-8');

let cachegetUint8Memory = null;
function getUint8Memory() {
    if (cachegetUint8Memory === null || cachegetUint8Memory.buffer !== _ui_bg__WEBPACK_IMPORTED_MODULE_0__["memory"].buffer) {
        cachegetUint8Memory = new Uint8Array(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["memory"].buffer);
    }
    return cachegetUint8Memory;
}

function getStringFromWasm(ptr, len) {
    return cachedDecoder.decode(getUint8Memory().subarray(ptr, ptr + len));
}

function __wbg_error_cc95a3d302735ca3(arg0, arg1) {
    let varg0 = getStringFromWasm(arg0, arg1);

    varg0 = varg0.slice();
    _ui_bg__WEBPACK_IMPORTED_MODULE_0__["__wbindgen_free"](arg0, arg1 * 1);

    __wbg_error_cc95a3d302735ca3_target(varg0);
}

let cachegetUint32Memory = null;
function getUint32Memory() {
    if (cachegetUint32Memory === null || cachegetUint32Memory.buffer !== _ui_bg__WEBPACK_IMPORTED_MODULE_0__["memory"].buffer) {
        cachegetUint32Memory = new Uint32Array(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["memory"].buffer);
    }
    return cachegetUint32Memory;
}

const slab = [{ obj: undefined }, { obj: null }, { obj: true }, { obj: false }];

let slab_next = slab.length;

function addHeapObject(obj) {
    if (slab_next === slab.length) slab.push(slab.length + 1);
    const idx = slab_next;
    const next = slab[idx];

    slab_next = next;

    slab[idx] = { obj, cnt: 1 };
    return idx << 1;
}

const stack = [];

function getObject(idx) {
    if ((idx & 1) === 1) {
        return stack[idx >> 1];
    } else {
        const val = slab[idx >> 1];

        return val.obj;

    }
}

function __widl_f_new_with_str_and_init_Request(arg0, arg1, arg2, exnptr) {
    let varg0 = getStringFromWasm(arg0, arg1);
    try {
        return addHeapObject(new Request(varg0, getObject(arg2)));
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

function __widl_instanceof_Response(idx) {
    return getObject(idx) instanceof Response ? 1 : 0;
}

const __widl_f_json_Response_target = Response.prototype.json || function() {
    throw new Error(`wasm-bindgen: Response.prototype.json does not exist`);
};

function __widl_f_json_Response(arg0, exnptr) {
    try {
        return addHeapObject(__widl_f_json_Response_target.call(getObject(arg0)));
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

function __widl_instanceof_Window(idx) {
    return getObject(idx) instanceof Window ? 1 : 0;
}

const __widl_f_fetch_with_request_Window_target = function(x0) {
    return this.fetch(x0);
};

function __widl_f_fetch_with_request_Window(arg0, arg1) {
    return addHeapObject(__widl_f_fetch_with_request_Window_target.call(getObject(arg0), getObject(arg1)));
}

function __wbg_newnoargs_b1f726fad978f5a3(arg0, arg1) {
    let varg0 = getStringFromWasm(arg0, arg1);
    return addHeapObject(new Function(varg0));
}

const __wbg_call_fa7f0da29d7b9250_target = Function.prototype.call || function() {
    throw new Error(`wasm-bindgen: Function.prototype.call does not exist`);
};

function __wbg_call_fa7f0da29d7b9250(arg0, arg1, exnptr) {
    try {
        return addHeapObject(__wbg_call_fa7f0da29d7b9250_target.call(getObject(arg0), getObject(arg1)));
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

const __wbg_call_bd08bd79389c3e82_target = Function.prototype.call || function() {
    throw new Error(`wasm-bindgen: Function.prototype.call does not exist`);
};

function __wbg_call_bd08bd79389c3e82(arg0, arg1, arg2, exnptr) {
    try {
        return addHeapObject(__wbg_call_bd08bd79389c3e82_target.call(getObject(arg0), getObject(arg1), getObject(arg2)));
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

function __wbg_new_70e5fc804058d6a0() {
    return addHeapObject(new Object());
}

const __wbg_set_3387446d3253486c_target = Reflect.set.bind(Reflect) || function() {
    throw new Error(`wasm-bindgen: Reflect.set.bind(Reflect) does not exist`);
};

function __wbg_set_3387446d3253486c(arg0, arg1, arg2, exnptr) {
    try {
        return __wbg_set_3387446d3253486c_target(getObject(arg0), getObject(arg1), getObject(arg2)) ? 1 : 0;
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

const __wbg_stringify_cc97edb3bc4849c0_target = JSON.stringify.bind(JSON) || function() {
    throw new Error(`wasm-bindgen: JSON.stringify.bind(JSON) does not exist`);
};

function __wbg_stringify_cc97edb3bc4849c0(arg0, exnptr) {
    try {
        return addHeapObject(__wbg_stringify_cc97edb3bc4849c0_target(getObject(arg0)));
    } catch (e) {
        const view = getUint32Memory();
        view[exnptr / 4] = 1;
        view[exnptr / 4 + 1] = addHeapObject(e);

    }
}

let cachedGlobalArgumentPtr = null;
function globalArgumentPtr() {
    if (cachedGlobalArgumentPtr === null) {
        cachedGlobalArgumentPtr = _ui_bg__WEBPACK_IMPORTED_MODULE_0__["__wbindgen_global_argument_ptr"]();
    }
    return cachedGlobalArgumentPtr;
}

function getGlobalArgument(arg) {
    const idx = globalArgumentPtr() / 4 + arg;
    return getUint32Memory()[idx];
}

function __wbg_new_477a6c8ec5821b36(arg0) {
    let cbarg0 = function(arg0, arg1) {
        let a = this.a;
        this.a = 0;
        try {
            return this.f(a, this.b, addHeapObject(arg0), addHeapObject(arg1));

        } finally {
            this.a = a;

        }

    };
    cbarg0.f = _ui_bg__WEBPACK_IMPORTED_MODULE_0__["__wbg_function_table"].get(arg0);
    cbarg0.a = getGlobalArgument(0);
    cbarg0.b = getGlobalArgument(0 + 1);
    try {
        return addHeapObject(new Promise(cbarg0.bind(cbarg0)));
    } finally {
        cbarg0.a = cbarg0.b = 0;

    }
}

const __wbg_then_b902ad50736efa21_target = Promise.prototype.then || function() {
    throw new Error(`wasm-bindgen: Promise.prototype.then does not exist`);
};

function __wbg_then_b902ad50736efa21(arg0, arg1, arg2) {
    return addHeapObject(__wbg_then_b902ad50736efa21_target.call(getObject(arg0), getObject(arg1), getObject(arg2)));
}

const __wbg_text_6ad386c63bfa0000_target = function(x0) {
    return this.text(x0);
};

function __wbg_text_6ad386c63bfa0000(arg0, arg1, arg2) {
    let varg1 = getStringFromWasm(arg1, arg2);
    return addHeapObject(__wbg_text_6ad386c63bfa0000_target.call(getObject(arg0), varg1));
}

function dropRef(idx) {

    idx = idx >> 1;
    if (idx < 4) return;
    let obj = slab[idx];

    obj.cnt -= 1;
    if (obj.cnt > 0) return;

    // If we hit 0 then free up our space in the slab
    slab[idx] = slab_next;
    slab_next = idx;
}

function takeObject(idx) {
    const ret = getObject(idx);
    dropRef(idx);
    return ret;
}
/**
* @param {number} arg0
* @returns {any}
*/
function ui_category_delete(arg0) {
    return takeObject(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["ui_category_delete"](arg0));
}

/**
* @param {number} arg0
* @returns {any}
*/
function ui_product_delete(arg0) {
    return takeObject(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["ui_product_delete"](arg0));
}

/**
* @param {number} arg0
* @returns {any}
*/
function ui_supplier_delete(arg0) {
    return takeObject(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["ui_supplier_delete"](arg0));
}

/**
* @param {any} arg0
* @returns {any}
*/
function ui_show_list(arg0) {
    return takeObject(_ui_bg__WEBPACK_IMPORTED_MODULE_0__["ui_show_list"](addHeapObject(arg0)));
}

function __wbindgen_object_clone_ref(idx) {
    // If this object is on the stack promote it to the heap.
    if ((idx & 1) === 1) return addHeapObject(getObject(idx));

    // Otherwise if the object is on the heap just bump the
    // refcount and move on
    const val = slab[idx >> 1];
    val.cnt += 1;
    return idx;
}

function __wbindgen_object_drop_ref(i) {
    dropRef(i);
}

function __wbindgen_string_new(p, l) {
    return addHeapObject(getStringFromWasm(p, l));
}

function __wbindgen_number_get(n, invalid) {
    let obj = getObject(n);
    if (typeof(obj) === 'number') return obj;
    getUint8Memory()[invalid] = 1;
    return 0;
}

function __wbindgen_is_null(idx) {
    return getObject(idx) === null ? 1 : 0;
}

function __wbindgen_is_undefined(idx) {
    return getObject(idx) === undefined ? 1 : 0;
}

function __wbindgen_boolean_get(i) {
    let v = getObject(i);
    if (typeof(v) === 'boolean') {
        return v ? 1 : 0;
    } else {
        return 2;
    }
}

function __wbindgen_is_symbol(i) {
    return typeof(getObject(i)) === 'symbol' ? 1 : 0;
}

const TextEncoder = typeof self === 'object' && self.TextEncoder
    ? self.TextEncoder
    : __webpack_require__(/*! util */ "./node_modules/util/util.js").TextEncoder;

let cachedEncoder = new TextEncoder('utf-8');

function passStringToWasm(arg) {

    const buf = cachedEncoder.encode(arg);
    const ptr = _ui_bg__WEBPACK_IMPORTED_MODULE_0__["__wbindgen_malloc"](buf.length);
    getUint8Memory().set(buf, ptr);
    return [ptr, buf.length];
}

function __wbindgen_string_get(i, len_ptr) {
    let obj = getObject(i);
    if (typeof(obj) !== 'string') return 0;
    const [ptr, len] = passStringToWasm(obj);
    getUint32Memory()[len_ptr / 4] = len;
    return ptr;
}

function __wbindgen_cb_drop(i) {
    let obj = getObject(i).original;
    obj.a = obj.b = 0;
    dropRef(i);
}

function __wbindgen_closure_wrapper137(ptr, f, _ignored) {
    let cb = function(arg0) {
        let a = this.a;
        this.a = 0;
        try {
            return this.f(a, addHeapObject(arg0));

        } finally {
            this.a = a;

        }

    };
    cb.f = _ui_bg__WEBPACK_IMPORTED_MODULE_0__["__wbg_function_table"].get(f);
    cb.a = ptr;
    let real = cb.bind(cb);
    real.original = cb;
    return addHeapObject(real);
}

function __wbindgen_throw(ptr, len) {
    throw new Error(getStringFromWasm(ptr, len));
}



/***/ }),

/***/ "./src/ui_bg.wasm":
/*!************************!*\
  !*** ./src/ui_bg.wasm ***!
  \************************/
/*! exports provided: memory, __indirect_function_table, __heap_base, __data_end, __wbindgen_global_argument_ptr, __wbindgen_malloc, __wbindgen_free, ui_category_delete, ui_product_delete, ui_supplier_delete, ui_show_list, __wbg_function_table */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
// Instantiate WebAssembly module
var wasmExports = __webpack_require__.w[module.i];
__webpack_require__.r(exports);
// export exports from WebAssembly module
for(var name in wasmExports) if(name != "__webpack_init__") exports[name] = wasmExports[name];
// exec imports from WebAssembly module (for esm order)
/* harmony import */ var m0 = __webpack_require__(/*! ./ui */ "./src/ui.js");


// exec wasm module
wasmExports["__webpack_init__"]()

/***/ })

}]);
//# sourceMappingURL=0.bundle.js.map