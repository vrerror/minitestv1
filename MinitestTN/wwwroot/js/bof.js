dropifyInit = idClass => {
    var scope = '';
    if (idClass !== undefined) {
        scope = idClass
    }

    $(scope + ' .dropify').dropify({
        messages: {
            default: rs('Drag and drop file or click here', 'ลากไฟล์วางหรือคลิกที่นี่'),
            replace: rs('Drag and drop file or click here for uploading', 'ลากไฟล์วางหรือคลิกที่นี่ เพื่ออัพโหลดใหม่'),
            remove: rs('Clear', 'เคลียร์'),
            error: rs('Cannot upload file.', 'ไม่สามารถอัพโหลดไฟล์นี้ได้')
        },
        error: {
            fileSize: rs('The file size is too big ({{ value }} max).', 'ไฟล์ต้องมีขนาดไม่เกิน ({{ value }})'),
            minWidth: rs('The image width is too small ({{ value }}}px min).', 'รูปต้องมีความกว้างอย่างน้อย ({{ value }}})'),
            maxWidth: rs('The image width is too big ({{ value }}}px max).', 'รูปต้องมีความกว้างไม่เกิน ({{ value }}})'),
            minHeight: rs('The image height is too small ({{ value }}}px min).', 'รูปต้องมีความสูงอย่างน้อย ({{ value }}})'),
            maxHeight: rs('The image height is too big ({{ value }}px max).', 'รูปต้องมีความกว้างไม่เกิน ({{ value }})')
        }
    })
}

dropifySetFile = (dr, file) => {
    dr.resetPreview()
    dr.clearElement()
    dr.settings.defaultFile = file
    dr.destroy()
    dr.init()
}

dropifyReset = idClass => {
    var scope = ""
    if (idClass !== undefined) {
        scope = idClass
    }

    $(scope + " .dropify-clear").trigger("click")
    $(scope + " .dropify-wrapper").removeClass("has-error")
}

nvl = value => (value === null || value === 'null') ? '' : value