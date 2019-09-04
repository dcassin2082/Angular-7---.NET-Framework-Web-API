import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import * as $ from 'jquery';
import { UserService } from '../user/shared/user.service';
import { DeucesWildService } from './shared/deuces-wild.service';
import { DeucesWild } from './shared/deuces-wild.model';
import { DeucesWildPayout } from './shared/deuces-wild-payout.model';
import { environment } from 'src/environments/environment';

declare var $: any;

@Component({
  selector: 'app-deuces-wild-poker',
  templateUrl: './deuces-wild-poker.component.html',
  styleUrls: ['./deuces-wild-poker.component.css']
})
export class DeucesWildPokerComponent implements OnInit {

  constructor(public userService: UserService, public deucesWildService: DeucesWildService, private http: HttpClient, private router: Router) { }

  bet: number = 0;
  arr: number[] = [0, 0, 0, 0, 0];
  stageOfGame: string = "deal";
  baseUrl: string = environment.production ?  "http://dcassin5938-001-site1.ctempurl.com/api/deuceswild" : "https://localhost:44331/api/deuceswild/";
  response: DeucesWild;
  paytable: DeucesWildPayout[];
  credits: number;
  username: string = localStorage.getItem('username');
  src: string;

  getMode() {
    if (environment.production) {
      this.src = 'http://dcassin5938-001-site1.ctempurl.com/ang7app/src/assets/images';
    }
    else {
      this.src = '../../assets/images';
    }
  }

  ngOnInit() {
    this.getMode();
    if (localStorage.getItem('token') == null) {
      this.router.navigate(['/user/login']);
    }
    else {
      this.userService.loggedIn = true;
      this.deucesWildService.getPaytable().subscribe(x => {
        this.response = x as DeucesWild;
        this.paytable = this.response.DeucesWildPaytable;
        this.credits = this.response.Credits;
      })
    }
    this.highlightBetColumn();
    $("#lblResult").text('BET 1 TO 5 CREDITS');
  }

  highlightBetColumn() {
    for (var i = 1; i <= 6; i++) {
      $("td").css('background', '#242448');
    }
    $(".td-" + this.bet).css('background', 'red');
  }

  changeBet() {
    if (this.bet <= 5) {
      ++this.bet;
      $("#lblBet").text(this.bet);
      $('#btnBetAmt').attr('value', 'BET ' + this.bet);
    }
    if (this.bet > 5) {
      this.bet = 1;
      $("#lblBet").text(this.bet);
      $('#btnBetAmt').attr('value', 'BET ' + this.bet);
    }
    this.highlightBetColumn();
  }

  betMax() {
    this.bet = 5;
    $("#lblBet").text(this.bet);
    $('#btnBetAmt').attr('value', 'BET ' + this.bet);
    this.highlightBetColumn();
    this.dealDraw();
  }

  hold(event: { target: any; }) {
    var target = event.target;
    var idAttr = target.attributes.id;
    var id = idAttr.nodeValue;
    var n = id[id.length - 1];
    if (document.getElementById('hold' + n).style.visibility == 'visible') {
      document.getElementById('hold' + n).style.visibility = 'hidden';
      this.arr[n] = 0;
    }
    else {
      document.getElementById('hold' + n).style.visibility = 'visible'
      document.getElementById('hold' + n).innerHTML = 'HELD';
      this.arr[n] = 1;
    }
  }

  clearHoldLabels() {
    for (var i = 0; i < 5; i++) {
      document.getElementById('hold' + i).innerHTML = '';
      document.getElementById('hold' + i).style.visibility = 'hidden';
    }
  }

  dealDraw() {
    this.getMode();
    var src = this.src;
    if (this.stageOfGame == 'deal') {
      for (var i = 0; i < this.arr.length; i++) {
        this.arr[i] = 0;
      }
      this.clearHoldLabels();
      $('#btnBetAmt').prop('disabled', true);
      $('#btnBetMax').prop('disabled', true);
      $('img').addClass('img-clickable').removeClass('img-unclickable');
      this.stageOfGame = 'draw';
      for (var i = 0; i <= 9; i++) {
        $("#tr-" + i).addClass("tr-yellow").removeClass("tr-white");
      }
      $.ajax({
        type: 'get',
        dataType: 'json',
        contentType: 'application/json',
        url: environment.production ?  "http://dcassin5938-001-site1.ctempurl.com/api/deuceswild/deal" : "https://localhost:44331/api/deuceswild/deal",
        data: {
          bet: this.bet,
          username: localStorage.getItem('username')
        },
        traditional: true,
        success: function (result) {
          for (var i = 0; i < result.HandDeucesWild.length; i++) {
            var card = '#card' + i;
            $(card).attr('src', src + '/' + result.HandDeucesWild[i].ImageName);
          }
          $("#btnDealDraw").prop("value", "DRAW");
          $("#lblResult").text(result.Message);
          $("#lblCredits").text(result.Credits);

          switch (result.Message) {
            case "3 OF A KIND":
              $("#tr-9").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "STRAIGHT":
              $("#tr-8").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "FLUSH":
              $("#tr-7").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "FULL HOUSE":
              $("#tr-6").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "4 OF A KIND":
              $("#tr-5").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "STRAIGHT FLUSH":
              $("#tr-4").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "5 OF A KIND":
              $("#tr-3").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WILD ROYAL FLUSH":
              $("#tr-2").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "FOUR DEUCES":
              $("#tr-1").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "ROYAL FLUSH NO DEUCES":
              $("#tr-0").addClass("tr-white").removeClass("tr-yellow");
              break;
          }
        }
      })
    }
    else {
      for (var i = 0; i <= 12; i++) {
        $("#tr-" + i).addClass("tr-yellow").removeClass("tr-white");
      }
      this.stageOfGame = 'deal';
      $('img').addClass('img-unclickable').removeClass('img-clickable');
      $('#btnDealDraw').attr('value', 'DEAL');
      $('#btnBetAmt').prop('disabled', false);
      $('#btnBetMax').prop('disabled', false);
      $.ajax({
        type: 'get',
        contentType: "application/json",
        dataType: 'json',
        url: environment.production ?  "http://dcassin5938-001-site1.ctempurl.com/api/deuceswild/draw/arr" : "https://localhost:44331/api/deuceswild/draw/arr",
        traditional: true,
        data: {
          arr: this.arr,
          bet: this.bet,
          username: localStorage.getItem('username')
        },
        success: function (result) {
          for (i = 0; i < result.HandDeucesWild.length; i++) {
            var card = '#card' + i;
            $(card).attr('src', src + '/' + result.HandDeucesWild[i].ImageName);
          }
          $("#btnDealDraw").prop("value", "DEAL");
          $("#lblResult").text(result.Message);
          $("#lblCredits").text(result.Credits);
          switch (result.Message) {
            case "WINNER - 3 OF A KIND":
              $("#tr-9").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - STRAIGHT":
              $("#tr-8").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - FLUSH":
              $("#tr-7").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - FULL HOUSE":
              $("#tr-6").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - 4 OF A KIND":
              $("#tr-5").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - STRAIGHT FLUSH":
              $("#tr-4").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - 5 OF A KIND":
              $("#tr-3").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - WILD ROYAL FLUSH":
              $("#tr-2").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - FOUR DEUCES":
              $("#tr-1").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - ROYAL FLUSH NO DEUCES":
              $("#tr-0").addClass("tr-white").removeClass("tr-yellow");
              break;
          }
          $("#lblWinAmt").text(result.WinAmount);
        }
      })
    }
  }
}
