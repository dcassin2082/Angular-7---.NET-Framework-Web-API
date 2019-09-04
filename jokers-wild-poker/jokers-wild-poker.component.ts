import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserService } from '../user/shared/user.service';
import { JokersWildService } from './shared/jokers-wild.service';
import { JokersWild } from './shared/jokers-wild.model';
import * as $ from 'jquery';
import { JokersWildPayout } from './shared/jokers-wild-payout.model';
import { environment } from 'src/environments/environment';

declare var $: any;

@Component({
  selector: 'app-jokers-wild-poker',
  templateUrl: './jokers-wild-poker.component.html',
  styleUrls: ['./jokers-wild-poker.component.css']
})
export class JokersWildPokerComponent implements OnInit {

  constructor(public userService: UserService, public jokersWildService: JokersWildService, private http: HttpClient, private router: Router) { }

  bet: number = 0;
  arr: number[] = [0, 0, 0, 0, 0];
  stageOfGame: string = "deal";
  baseUrl: string = environment.production ?  "http://dcassin5938-001-site1.ctempurl.com/api/jokerswild" : "https://localhost:44331/api/jokerswild/";
  response: JokersWild;
  paytable: JokersWildPayout[];
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
      this.jokersWildService.getPaytable().subscribe(x => {
        this.response = x as JokersWild;
        this.paytable = this.response.JokersWildPaytable;
        this.credits = this.response.Credits;
      })
    }
    this.highlightBetColumn();
    $("#lblResult").text('BET 1 TO 5 CREDITS');
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

  highlightBetColumn() {
    for (var i = 1; i <= 6; i++) {
      $("td").css('background', '#242448');
    }
    $(".td-" + this.bet).css('background', 'red');
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
      for (var i = 0; i <= 12; i++) {
        $("#tr-" + i).addClass("tr-yellow").removeClass("tr-white");
      }
      $.ajax({
        type: 'get',
        dataType: 'json',
        contentType: 'application/json',
        url: environment.production ? 'http://dcassin5938-001-site1.ctempurl.com/api/jokerswild/deal' : 'https://localhost:44331/api/jokerswild/deal', 
        data: {
          bet: this.bet,
          username: localStorage.getItem('username')
        },
        traditional: true,
        success: function (result) {
          for (var i = 0; i < result.Hand.length; i++) {
            var card = '#card' + i;
            $(card).attr('src', src + '/' + result.Hand[i].ImageName);
          }
          $("#btnDealDraw").prop("value", "DRAW");
          $("#lblResult").text(result.Message);
          $("#lblCredits").text(result.Credits);

          switch (result.Message) {
            case "PAIR OF KINGS OR BETTER":
              $("#tr-10").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "TWO PAIR":
              $("#tr-9").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "3 OF A KIND":
              $("#tr-8").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "STRAIGHT":
              $("#tr-7").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "FLUSH":
              $("#tr-6").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "FULL HOUSE":
              $("#tr-5").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "4 OF A KIND":
              $("#tr-4").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "STRAIGHT FLUSH":
              $("#tr-3").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "ROYAL FLUSH WITH JOKER":
              $("#tr-2").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "5 OF A KIND":
              $("#tr-1").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "ROYAL FLUSH NO JOKERS":
              $("#tr-0").addClass("tr-white").removeClass("tr-yellow");
              break;
          }
        }
      })
    }
    else {
      for (var i = 0; i <= 10; i++) {
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
        url: environment.production ? 'http://dcassin5938-001-site1.ctempurl.com/api/jokerswild/draw/arr' : 'https://localhost:44331/api/jokerswild/draw/arr', 
        traditional: true,
        data: {
          arr: this.arr,
          bet: this.bet,
          username: localStorage.getItem('username')
        },
        success: function (result) {
          for (i = 0; i < result.Hand.length; i++) {
            var card = '#card' + i;
            $(card).attr('src', src + '/' + result.Hand[i].ImageName);
          }
          $("#btnDealDraw").prop("value", "DEAL");
          $("#lblResult").text(result.Message);
          $("#lblCredits").text(result.Credits);
          switch (result.Message) {
            case "WINNER - PAIR OF KINGS OR BETTER":
              $("#tr-10").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - TWO PAIR":
              $("#tr-9").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - 3 OF A KIND":
              $("#tr-8").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - STRAIGHT":
              $("#tr-7").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - FLUSH":
              $("#tr-6").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - FULL HOUSE":
              $("#tr-5").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - 4 OF A KIND":
              $("#tr-4").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - STRAIGHT FLUSH":
              $("#tr-3").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - ROYAL FLUSH WITH JOKER":
              $("#tr-2").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - 5 OF A KIND":
              $("#tr-1").addClass("tr-white").removeClass("tr-yellow");
              break;
            case "WINNER - ROYAL FLUSH NO JOKERS":
              $("#tr-0").addClass("tr-white").removeClass("tr-yellow");
              break;
          }
          $("#lblWinAmt").text(result.WinAmount);
        }
      })
    }
  }

  clearHoldLabels() {
    for (var i = 0; i < 5; i++) {
      document.getElementById('hold' + i).innerHTML = '';
      document.getElementById('hold' + i).style.visibility = 'hidden';
    }
  }
}
