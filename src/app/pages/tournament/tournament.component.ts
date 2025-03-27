import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Button } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { FloatLabel } from 'primeng/floatlabel';
import { Calendar } from 'primeng/calendar';
import { Card } from 'primeng/card';
import { ToggleSwitch } from 'primeng/toggleswitch';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Select } from 'primeng/select';

@Component({
  selector: 'app-tournament',
  imports: [CommonModule, Button, InputText, FloatLabel, Calendar, Card, ReactiveFormsModule, Select, ToggleSwitch],
  templateUrl: './tournament.component.html',
  styleUrl: './tournament.component.scss'
})
export class TournamentComponent {

  fb = inject(FormBuilder);

  categories = [
    { label: 'Catégories*', value: null },
    { label: 'Junior', value: 0 },
    { label: 'Senior', value: 1 },
    { label: 'Veteran', value: 2 },
  ]

  tournamentForm = this.fb.group({
    name: [null, [Validators.required]],
    location: [null],
    player: [null],
    elo: [null],
    category: [null, [Validators.required]],
    registrationEndDate: [null, [Validators.required]],
    womenOnly: []
  })

  submit() {}
}
