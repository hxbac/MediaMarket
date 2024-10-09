import { CKEditor } from '@ckeditor/ckeditor5-react';
import { ClassicEditor, Bold, Essentials, Italic, Mention, Paragraph, Undo, Image, ImageCaption, ImageResize, ImageStyle, ImageToolbar, LinkImage, ImageUpload } from 'ckeditor5';

import 'ckeditor5/ckeditor5.css';

export default function CustomEditor() {
  return (
    <CKEditor
      editor={ ClassicEditor }
      config={ {
        toolbar: {
            items: [ 'undo', 'redo', '|', 'bold', 'italic', 'insertImage' ],
        },
        plugins: [
            Bold, Essentials, Italic, Mention, Paragraph, Undo, Image, ImageToolbar, ImageCaption, ImageStyle, ImageResize, LinkImage, ImageUpload
        ],
        initialData: '<p>Hello from CKEditor 5 in React!</p>',
      } }
    />
  );
}
