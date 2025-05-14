import { Component, input } from '@angular/core';
import { Member } from '../../_models/member';
import { Photo } from '../../_models/photo';

@Component({
  selector: 'app-photo-editor',
  imports: [],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css'
})
export class PhotoEditorComponent {
deletePhoto(arg0: number) {
throw new Error('Method not implemented.');
}
setMainPhoto(_t2: Photo) {
throw new Error('Method not implemented.');
}
uploader: any;
fileOverBase($event: Event) {
throw new Error('Method not implemented.');
}

  member = input.required<Member>();
hasBaseDropZoneOver: any;

}
