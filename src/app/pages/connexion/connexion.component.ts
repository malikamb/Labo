import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Button } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { FloatLabel } from 'primeng/floatlabel';
import { Card } from 'primeng/card';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-connexion',
  imports: [CommonModule, Button, InputText, FloatLabel, Card, ReactiveFormsModule],
  templateUrl: './connexion.component.html',
  styleUrl: './connexion.component.scss'
})
export class ConnexionComponent {
  fb = inject(FormBuilder);

  ConnexionForm = this.fb.group({})

  submit(){

  }
}
