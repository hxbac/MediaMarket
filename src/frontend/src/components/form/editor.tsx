import { CKEditor } from '@ckeditor/ckeditor5-react';
import { ClassicEditor, Bold, Essentials, Italic, Mention, Paragraph, Undo, Image, ImageCaption, ImageResize, ImageStyle, ImageToolbar, LinkImage, ImageUpload, Editor, EventInfo } from 'ckeditor5';

// Todo import once
import 'ckeditor5/ckeditor5.css';

export default function CustomEditor({ value, onChange }: { value: string, onChange: (value: string) => void }) {
  const handleEditorChange = (event: EventInfo<string, unknown>, editor: Editor) => {
    const data = editor.getData();
    onChange(data);
  };

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
      } }
      data={value}
      onChange={handleEditorChange}
    />
  );
}
